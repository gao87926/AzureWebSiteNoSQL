using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using imagesharingwithcloudserviceWebRole.Models;
using WebMatrix.WebData;
using System.Web.Security;


namespace imagesharingwithcloudserviceWebRole.DAL
{
    // public class ImageSharingDBInitializer : DropCreateDatabaseAlways<ImageSharingDB>
    public class ImageSharingDBInitializer : IDatabaseInitializer<ImageSharingDB>
    {
        public void InitializeDatabase(ImageSharingDB db)
        {
            if (db.Database.Exists())
            {
                //db.Database.ExecuteSqlCommand("alter database ImageSharingWithCloudStorage set single_user with rollback immediate");
                db.Database.Delete();
            }
            db.Database.Create();
            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection", "Users", "Id", "UserId", autoCreateTables: true);
            this.Seed(db);
        }
        protected void Seed(ImageSharingDB db)
        {
            if (!WebSecurity.UserExists("nobody"))
            {
                WebSecurity.CreateUserAndAccount("nobody", "nobody1234", new { ADA = "false", Active = true });

            }
            if (!WebSecurity.UserExists("jfk"))
            {
                WebSecurity.CreateUserAndAccount("jfk", "jfk1234", new { ADA = "false", Active = true });

            }
            if (!WebSecurity.UserExists("nixon"))
            {
                WebSecurity.CreateUserAndAccount("nixon", "nixon1234", new { ADA = "false", Active = true });

            }
            if (!WebSecurity.UserExists("fdr"))
            {
                WebSecurity.CreateUserAndAccount("fdr", "fdr1234", new { ADA = "false", Active = true });

            }
            db.Tags.Add(new Tag { Name = "portrait" });
            db.Tags.Add(new Tag { Name = "architecture" });
            if (!Roles.RoleExists("User"))
                Roles.CreateRole("User");
            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");
            if (!Roles.RoleExists("Approver"))
                Roles.CreateRole("Approver");
            if (!Roles.RoleExists("Supervisor"))
                Roles.CreateRole("Supervisor");
            db.SaveChanges();

            if (!Roles.GetRolesForUser("jfk").Contains("Approver"))
                Roles.AddUserToRole("jfk", "Approver");
            if (!Roles.GetRolesForUser("fdr").Contains("Supervisor"))
                Roles.AddUserToRole("fdr", "Supervisor");
            if (!Roles.GetRolesForUser("nixon").Contains("Administrator"))
                Roles.AddUserToRole("nixon", "Administrator");
            db.SaveChanges();

            db.Images.Add(new Image
            {
                Caption = "Ingrid Bergman",
                Description = "Best remembered for her role in Casablanca, "
                + "even though she considered some of her "
                + " other films to be more important",
                DateTaken = new DateTime(1946, 12, 14),
                UserId = 1,
                TagId = 1,
                Approved = true,
                Valid = true
            });
            db.SaveChanges();

            LogContext.CreateTable();


        }
    }
}