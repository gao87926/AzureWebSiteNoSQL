using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace imagesharingwithcloudserviceWorkerRole
{
    class StorageQueueConnector
    {
        public static CloudQueue CreateQueuesIfNotCreated(string userId)
        {

            string accountName = ConfigurationManager.AppSettings["azurestorageaccount"];
            string accountKey = ConfigurationManager.AppSettings["azurestoragekeystring"];

            StorageCredentials credentials = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(credentials, true);

            CloudQueueClient queueClient = account.CreateCloudQueueClient();
            //queue name need be lower case
            CloudQueue qRef = queueClient.GetQueueReference("user-" + userId);
            qRef.CreateIfNotExists();
            return qRef;



        }
    }
}
