using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Core;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;

namespace imagesharingwithcloudserviceWebRole.Models
{
    public class LogEntry : TableServiceEntity
    {
        public LogEntry() { CreateKeys(0); }
        public LogEntry(int imageId)
        {
            CreateKeys(imageId);
        }



        public DateTime EntryDate { get; set; }
        public string Userid { get; set; }
        public string Caption { get; set; }
        public string Uri { get; set; }
        public int ImageId { get; set; }

        public void CreateKeys(int imageId)
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime targetTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, est);
            EntryDate = targetTime;

            PartitionKey = EntryDate.ToString("MMddyyyy");

            RowKey = string.Format("{0}-s{1:10}_{2}",
                this.ImageId = imageId,
                DateTime.MaxValue.Ticks - EntryDate.Ticks,
                Guid.NewGuid());
        }

    }
}