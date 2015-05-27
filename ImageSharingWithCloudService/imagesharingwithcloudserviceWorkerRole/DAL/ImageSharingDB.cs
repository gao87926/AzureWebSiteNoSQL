using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using imagesharingwithcloudserviceWebRole.Models;
using System.Data.Entity;

namespace imagesharingwithcloudserviceWebRole.DAL
{
    public class ImageSharingDB : DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public ImageSharingDB() : base("DefaultConnection") { }

    }
}