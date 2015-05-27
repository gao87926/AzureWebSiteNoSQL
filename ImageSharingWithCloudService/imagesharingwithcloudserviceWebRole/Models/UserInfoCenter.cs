using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imagesharingwithcloudserviceWebRole.Models
{
    public class UserInfoCenter
    {
        public string Id { get; set; }
        public string PopReceipt { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool Checked { get; set; }

        public UserInfoCenter(String id, String popReceipt , String name,String status, bool c)
        {
            this.Id = id;
            this.PopReceipt = popReceipt;
            this.Name = name;
            this.Status = status;
            this.Checked = c;
        }

        public UserInfoCenter() { }
    }
}