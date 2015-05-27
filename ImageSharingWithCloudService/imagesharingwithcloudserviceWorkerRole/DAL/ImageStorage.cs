using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Core;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using System.IO;

namespace imagesharingwithcloudserviceWorkerRole.DAL
{
    public class ImageStorage
    {
        public const bool USER_BLOB_STORAGE = true;

        public static string ACCOUNT = ConfigurationManager.AppSettings["azurestorageaccount"];

        public static string CONTAINER = ConfigurationManager.AppSettings["azurestoragecontainer"];

        public static void DeleteFile(HttpServerUtilityBase server, int imageId)
        {
            string accountName = ACCOUNT;
            string accountKey = ConfigurationManager.AppSettings["azurestoragekeystring"];

            StorageCredentials credentials = new StorageCredentials(accountName, accountKey);

            CloudStorageAccount account = new CloudStorageAccount(credentials, true);


            CloudBlobClient client = account.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference("images");

            CloudBlockBlob blob = container.GetBlockBlobReference(FilePath(server, imageId));

            blob.Delete();

        }
        public static void DeleteFile(string url, int imageId)
        {
            string accountName = ACCOUNT;
            string accountKey = ConfigurationManager.AppSettings["azurestoragekeystring"];

            StorageCredentials credentials = new StorageCredentials(accountName, accountKey);

            CloudStorageAccount account = new CloudStorageAccount(credentials, true);


            CloudBlobClient client = account.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference("images");

            CloudBlockBlob blob = container.GetBlockBlobReference(url);

            blob.Delete();

        }

        public static void SaveFile(HttpServerUtilityBase server, HttpPostedFileBase imageFile,
            int imageId)
        {
            if (USER_BLOB_STORAGE)
            {
                string accountName = ACCOUNT;
                string accountKey = ConfigurationManager.AppSettings["azurestoragekeystring"];

                StorageCredentials credentials = new StorageCredentials(accountName, accountKey);

                CloudStorageAccount account = new CloudStorageAccount(credentials, true);


                CloudBlobClient client = account.CreateCloudBlobClient();

                CloudBlobContainer container = client.GetContainerReference("images");

                CloudBlockBlob blob = container.GetBlockBlobReference(FilePath(server, imageId));
                imageFile.InputStream.Seek(0, SeekOrigin.Begin);
                blob.UploadFromStream(imageFile.InputStream);
            }
            else
            {
                string imgFileName = FilePath(server, imageId);
                imageFile.SaveAs(imgFileName);
            }
        }
        public static string FilePath(HttpServerUtilityBase server, int imageId)
        {
            if (USER_BLOB_STORAGE)
            {
                return FileName(imageId);

            }
            else
            {
                string imgFileName = server.MapPath("~/Content/Images/" + FileName(imageId));
                return imgFileName;
            }
        }
        public static string FileName(int imageId)
        {
            return imageId + ".jpg";
        }

        public static string ImageURI(UrlHelper urlHelper, int imageId)
        {
            if (USER_BLOB_STORAGE)
            {
                return "http://" + ACCOUNT + ".blob.core.windows.net/" + CONTAINER + "/" + FileName(imageId);
            }
            else
            {
                return urlHelper.Content("~/Content/Images/" + FileName(imageId));
            }
        }
    }
}