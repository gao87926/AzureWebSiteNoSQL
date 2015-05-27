using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using imagesharingwithcloudserviceWebRole.Models;
using System.IO;
using imagesharingwithcloudserviceWebRole;
using imagesharingwithcloudserviceWorkerRole.DAL;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace imagesharingwithcloudserviceWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        const string QueueName = "imgmsg";
        bool result = false;
        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            // Initiates the message pump and callback is invoked for each message that is received, 
            //calling close on the client will stop the pump.
            Client.OnMessage((receivedMessage) =>
                {
                    try
                    {
                        Trace.WriteLine("Processing", receivedMessage.SequenceNumber.ToString());
                        // View the message as an OnlineOrder
                        imgmsg img = receivedMessage.GetBody<imgmsg>();
                        Trace.WriteLine(img.Url + ": " + "/n" + img.Caption, "ProcessingMessage");
                        receivedMessage.Complete();
                        using(WebClient client = new WebClient())
                        {
                           byte[] myDataBuffer = client.DownloadData(img.Url);
                           Stream imgStream =new MemoryStream(myDataBuffer);
                           result =  ValidateImg.ValidateImage(imgStream);
                        }
                        if (!result)
                        {
                            // HttpServerUtilityBase server = new HttpServerUtilityWrapper(System.Web.HttpContext.Current.Server);
                            ImageStorage.DeleteFile(img.Url, int.Parse(img.Id));
                            DBModify.DeleteImg(int.Parse(img.Id));

                        }
                        else
                        {
                            DBModify.setValid(int.Parse(img.Id));
                            CloudQueue qRef = StorageQueueConnector.CreateQueuesIfNotCreated(img.UserId);
                            CloudQueueMessage msg = new CloudQueueMessage(string.Format("{0}:{1}", img.Caption,"Validated"));
                            qRef.AddMessage(msg);
                        }
                       

                    }
                    catch
                    {
                        // Handle any message processing specific exceptions here
                    }
                });

            CompletedEvent.WaitOne();
        }

     
        public override bool OnStart()
        {
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            return base.OnStart();
        }

      
        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }

    }
}
