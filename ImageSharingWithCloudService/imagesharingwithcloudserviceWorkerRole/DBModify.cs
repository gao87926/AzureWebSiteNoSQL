using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using imagesharingwithcloudserviceWebRole;
using imagesharingwithcloudserviceWebRole.DAL;
using imagesharingwithcloudserviceWebRole.Models;

namespace imagesharingwithcloudserviceWebRole
{
    class DBModify
    {
        public static void DeleteImg(int imgId)
        {
            using (var db = new ImageSharingDB())
            {
                Image imageEntity = db.Images.Find(imgId);

               
                    //db.Entry(imageEntity).State = EntityState.Deleted;
                    db.Images.Remove(imageEntity);
                    db.SaveChanges();
                                   
            }
        }

        public static void setValid(int imgId)
        {
            using (var db = new ImageSharingDB())
            {
                Image imageEntity = db.Images.Find(imgId);


                //db.Entry(imageEntity).State = EntityState.Deleted;
                imageEntity.Valid = true;
                db.SaveChanges();

            }
        }
    }
}
