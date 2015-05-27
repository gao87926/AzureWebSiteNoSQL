using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imagesharingwithcloudserviceWebRole.Models
{
    public class SelectItemView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public SelectItemView(int id, String name, bool c)
        {
            this.Id = id;
            this.Name = name;
            this.Checked = c;
        }

        public SelectItemView() { }
    }
}