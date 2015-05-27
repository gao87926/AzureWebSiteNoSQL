using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imagesharingwithcloudserviceWebRole.Models
{
    public class imgmsg
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Url {get; set;}
        public string Caption { get; set; }
    }
}