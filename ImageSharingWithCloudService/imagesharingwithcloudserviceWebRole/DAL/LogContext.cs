using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Core;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using imagesharingwithcloudserviceWebRole.Models;
using System.Configuration;

namespace imagesharingwithcloudserviceWebRole.DAL
{
    public class LogContext
    {
        public const string LOG_TABLE_NAME = "imageviews";

        protected TableServiceContext context;

        public LogContext(TableServiceContext context)
        {
            this.context = context;
        }

        public void addLogEntry(string user, ImageView image)
        {
            LogEntry entry = new LogEntry(image.Id);
            entry.Userid = user;
            entry.Caption = image.Caption;
            entry.ImageId = image.Id;
            entry.Uri = image.Uri;
            context.AddObject(LOG_TABLE_NAME, entry);
            context.SaveChangesWithRetries();


        }

        protected static CloudTableClient GetClient()
        {
            string accountName = ConfigurationManager.AppSettings["azurestorageaccount"];
            string accountKey = ConfigurationManager.AppSettings["azurestoragekeystring"];
            StorageCredentials credential = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(credential, true);

            CloudTableClient client = account.CreateCloudTableClient();
            return client;
        }

        protected static LogContext GetContext()
        {


            CloudTableClient client = GetClient();
            LogContext context = new LogContext(client.GetTableServiceContext());
            return context;

        }

        public static void CreateTable()
        {
            CloudTableClient client = GetClient();

            CloudTable table = client.GetTableReference(LOG_TABLE_NAME);
            table.CreateIfNotExists();



        }
        public IEnumerable<LogEntry> select(DateTime queryDate)
        {

            var results = from entity in context.CreateQuery<LogEntry>(LOG_TABLE_NAME)
                          where entity.PartitionKey.Equals(queryDate.ToString("MMddyyyy"))
                          select entity;
            return results.ToList();
        }



        public static void AddLogEntry(string user, ImageView image)
        {
            LogContext context = GetContext();
            context.addLogEntry(user, image);
        }

        public static IEnumerable<LogEntry> Select(DateTime queryDate)
        {
            LogContext context = GetContext();
            return context.select(queryDate);
        }
    }
}