using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;
using Microsoft.WindowsAzure;

namespace imagesharingwithcloudserviceWebRole
{

    public static class BusQueueConnector
    {
        // Thread-safe. Recommended that you cache rather than recreating it
        // on every request.
        public static QueueClient ImageCheckQueueClient;


        // Obtain these values from the Management Portal

        static string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

        // The name of your queue
        public const string QueueName = "imgmsg";


        public static NamespaceManager CreateNamespaceManager()
        {
            // Create the namespace manager which gives you access to
            // management operations
            return NamespaceManager.CreateFromConnectionString(connectionString);
        }


        public static void Initialize()
        {
            // Using Http to be friendly with outbound firewalls
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;


            // Create the namespace manager which gives you access to 
            // management operations
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);


            // Create the queue if it does not exist already
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }


            // Get a client to the queue
            ImageCheckQueueClient = QueueClient.CreateFromConnectionString(connectionString, QueueName);
        }
    }
}

