using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using imagesharingwithcloudserviceWorkerRole;
using imagesharingwithcloudserviceWebRole.Models;
using imagesharingwithcloudserviceWebRole.DAL;
using System.Data;
using System.Data.Entity;
using Microsoft.Security.Application;
using System.Web.Security;
using WebMatrix.WebData;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;
using Microsoft.WindowsAzure.Storage.Queue;

namespace imagesharingwithcloudserviceWebRole.Controllers
{
    public class ImagesController : BaseController
    {
        private ImageSharingDB db = new ImageSharingDB();

        [HttpGet]
        public ActionResult Upload()
        {

            CheckAda();
            ViewBag.Message = "";
            ViewBag.Tags = new SelectList(db.Tags, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(ImageView image,
                                    HttpPostedFileBase ImageFile)
        {
            CheckAda();
            imgmsg imgmsg = new imgmsg();
            ViewBag.Tags = new SelectList(db.Tags, "Id", "Name");
            TryUpdateModel(image);
            if (ModelState.IsValid)
            {
                String userid = GetLoggedInUser();
                User user = db.Users.SingleOrDefault(u => u.Userid.Equals(userid));
                if (user != null)
                {
                    //try
                    //{
                    //   // System.Drawing.Image img = System.Drawing.Image.FromStream(ImageFile.InputStream);
                    //    if (!ValidateImage(ImageFile.InputStream))
                    //    {
                    //        return RedirectToAction("Error", "Home", new { userid = userid, errid = "ioFail" });
                    //    }
                    //}
                    //catch { return RedirectToAction("Error", "Home", new { userid = userid, errid = "ioFail" }); }

                    Image imageEntity = new Image();
                    try
                    {
                        imageEntity.Caption = Sanitizer.GetSafeHtmlFragment(image.Caption);
                        imageEntity.Description = Sanitizer.GetSafeHtmlFragment(image.Description);
                        imageEntity.DateTaken = image.DateTaken;
                        imageEntity.User = user;
                        imageEntity.Approved = false;

                        imageEntity.TagId = image.TagId;
                    }
                    catch (HttpRequestValidationException e)
                    {
                        return RedirectToAction("Error", "Home", new { errid = "Inputs data has issue" });
                    }


                    if (ImageFile != null && ImageFile.ContentLength > 0
                        && ImageFile.ContentLength < 1024000
                            && ImageFile.ContentType == "image/jpeg")
                    {
                        //commit image change
                        db.Images.Add(imageEntity);
                        db.SaveChanges();

                        ImageStorage.SaveFile(Server, ImageFile, imageEntity.Id);

                        imgmsg.Url = ImageStorage.ImageURI(Url, imageEntity.Id);
                        imgmsg.Caption = imageEntity.Caption;

                        imgmsg.Id = imageEntity.Id.ToString();

                        imgmsg.UserId = db.Users.Find(imageEntity.UserId).Userid;

                        // Create a message from the order
                        var message = new BrokeredMessage(imgmsg);
                        // Submit the order
                        BusQueueConnector.ImageCheckQueueClient.Send(message);

                        //String imgFileName = Server.MapPath("~/Images/img-" + imageEntity.Id + ".jpg");

                        //ImageFile.SaveAs(imgFileName);

                        //imageEntity.FileLocate = imgFileName;
                        //db.SaveChanges();


                        return RedirectToAction("Details", new { Id = imageEntity.Id });
                    }
                    else
                    {
                        ViewBag.Message = "No image file specified!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "No such userid registerd!";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Please correct the errors in the form!";
                return View();
            }
        }
        [HttpGet]
        public ActionResult Query()
        {
            CheckAda();
            ViewBag.Message = "";
            return View();
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            CheckAda();
            bool isApprover = false;
            string[] rolesArray;
            rolesArray = Roles.GetRolesForUser();
            foreach(string role in rolesArray){
                if (role == "Approver")
                {
                    isApprover = true;
                }
            }
            Image imageEntity = db.Images.Find(Id);
            if (imageEntity != null)
            {
                if (imageEntity.Approved == true || isApprover)
                {
                    ImageView imageView = new ImageView();
                    imageView.Id = imageEntity.Id;
                    imageView.Caption = imageEntity.Caption;
                    imageView.Description = imageEntity.Description;
                    imageView.DateTaken = imageEntity.DateTaken;
                    imageView.TagName = imageEntity.Tag.Name;
                    imageView.Userid = imageEntity.User.Userid;
                    imageView.Uri = ImageStorage.ImageURI(Url, imageEntity.Id);
                    //imageView.FileLocate = imageEntity.FileLocate;
                    LogContext.AddLogEntry(WebSecurity.CurrentUserName, imageView);
                    return View(imageView);
                 
                }
                else
                {
                    return RedirectToAction("Index", "Home");
              
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { errid = "Details" });
            }



        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            CheckAda();
            //if (GetLoggedInUser() == null)
            //{
            //    return ForceLogin();
            //}
            //else
            {

                Image imageEntity = db.Images.Find(Id);
                if (imageEntity != null)
                {
                    String userid = GetLoggedInUser();
                    if (imageEntity.User.Userid.Equals(userid))
                    {
                        ViewBag.Message = "";
                        ViewBag.Tags = new SelectList(db.Tags, "Id", "Name", imageEntity.TagId);
                        ImageView image = new ImageView();
                        image.Id = imageEntity.Id;
                        image.Uri = ImageStorage.ImageURI(Url, imageEntity.Id);
                        image.TagId = imageEntity.Id;
                        image.Caption = imageEntity.Caption;
                        image.Description = imageEntity.Description;
                        image.DateTaken = imageEntity.DateTaken;
                        return View("Edit", image);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { errid = "EditNotAuth" });

                    }
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "EditNotFound" });
                }
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, ImageView image)
        {
            CheckAda();
            ViewBag.Tags = new SelectList(db.Tags, "Id", "Name");
            String userid = GetLoggedInUser();
            //if (userid == null)
            //{
            //    return ForceLogin();
            //}
            //else
            {
                if (ModelState.IsValid)
                {
                    Image imageEntity = db.Images.Find(Id);
                    if (imageEntity != null)
                    {
                        if (imageEntity.User.Userid.Equals(userid))
                        {
                            try
                            {
                                imageEntity.TagId = image.TagId;
                                imageEntity.Caption = image.Caption;
                                imageEntity.Description = Sanitizer.GetSafeHtmlFragment(image.Description);
                                imageEntity.DateTaken = image.DateTaken;
                                db.Entry(imageEntity).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            catch (HttpRequestValidationException e)
                            {
                                return RedirectToAction("Error", "Home", new { errid = "Inputs data has issue" });
                            }
                            return RedirectToAction("Details", new { Id = Id });
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home", new { errid = "EditNotAuth" });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { errid = "EditNotFound" });
                    }
                }
                else
                {
                    return View("Edit", image);
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            CheckAda();
            //if (GetLoggedInUser() == null)
            //{
            //    return ForceLogin();
            //}
            //else
            {

                Image imageEntity = db.Images.Find(Id);
                if (imageEntity != null)
                {
                    ImageView imageView = new ImageView();
                    imageView.Id = imageEntity.Id;
                    imageView.Caption = imageEntity.Caption;
                    imageView.Description = imageEntity.Description;
                    imageView.DateTaken = imageEntity.DateTaken;
                    imageView.TagName = imageEntity.Tag.Name;
                    imageView.Userid = imageEntity.User.Userid;
                    return View(imageView);
                }
                else
                {

                    return RedirectToAction("Error", "Home", new { errid = "Delete" });
                }

            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection value, int Id)
        {
            CheckAda();
            String userid = GetLoggedInUser();
            //if (userid == null)
            //{
            //    return ForceLogin();
            //}
            //else
            {
                Image imageEntity = db.Images.Find(Id);
                if (imageEntity != null)
                {

                    if (imageEntity.User.Userid.Equals(userid))
                    {
                        //db.Entry(imageEntity).State = EntityState.Deleted;
                        db.Images.Remove(imageEntity);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { errid = "DeleteNotAuth" });
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "DeleteNotFound" });
                }
            }

        }
        [HttpGet]
        public ActionResult ListAll()
        {
            CheckAda();

            ViewBag.Userid = GetLoggedInUser();
            CheckAda();
            IEnumerable<Image> images = ApprovedImages().ToList();
            String userid = GetLoggedInUser();

            return View(images);


        }
        [HttpGet]
        public ActionResult ListByUser()
        {
            CheckAda();
            SelectList users = new SelectList(db.Users, "Id", "Userid", 1);
            return View(users);



        }
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult DoListByUser(int Id)
        {
            CheckAda();
            String userid = GetLoggedInUser();
            if (userid != null)
            {
                User user = db.Users.Find(Id);
                if (user != null)
                {
                    ViewBag.Userid = userid;
                    return View("ListAll", ApprovedImages(user.Images).ToList());
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { errid = "ListByUser" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpGet]
        public ActionResult ListByTag()
        {
            CheckAda();
            SelectList tags = new SelectList(db.Tags, "Id", "Name", 1);
            return View(tags);



        }
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult DoListByTag(int Id)
        {
            CheckAda();
            String userid = GetLoggedInUser();

            Tag tag = db.Tags.Find(Id);
            if (tag != null)
            {
                ViewBag.Userid = userid;
                return View("ListAll", ApprovedImages(tag.Images).ToList());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { errid = "ListByTag" });
            }


        }

        [HttpGet]
        [Authorize(Roles = "Approver")]
        public ActionResult Approve()
        {
            CheckAda();
            ViewBag.Message = "";
            var db = new ImageSharingDB();
            List<SelectItemView> model = new List<SelectItemView>();
            foreach (var u in db.Images)
            {
                if (!u.Approved && u.Valid)
                {
                    model.Add(new SelectItemView(u.Id, u.Caption, !u.Approved));
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Approver")]
        public ActionResult Approve(List<SelectItemView> model)
        {

            CheckAda();
            var db = new ImageSharingDB();

            foreach (var imod in model)
            {
                Image image = db.Images.Find(imod.Id);

                if (imod.Checked)
                {/* 
                  * Approve the Image
                  */
                    image.Approved = true;
                    CloudQueue qRef = StorageQueueConnector.CreateQueuesIfNotCreated(db.Users.Find(image.UserId).Userid);
                    CloudQueueMessage msg = new CloudQueueMessage(string.Format("{0}:{1}", image.Caption, "Approved"));
                    qRef.AddMessage(msg);
                    // model.Remove(imod);
                }
                imod.Name = image.Caption;
            }

            db.SaveChanges();
            ViewBag.Message = "Images Approved.";
            return View(model);
        }
        [HttpGet]
        public ActionResult InfoCenter()
        {
            CheckAda();
            ViewBag.Message = "";
            string userId = GetLoggedInUser();
            var db = new ImageSharingDB();
            CloudQueue msgQueue = StorageQueueConnector.CreateQueuesIfNotCreated(userId);
            List<UserInfoCenter> model = new List<UserInfoCenter>();
            foreach (CloudQueueMessage msg in msgQueue.GetMessages(10))
            {
               
                string details = msg.AsString;
                string[] detailSplit = details.Split(':');
                model.Add(new UserInfoCenter(msg.Id, msg.PopReceipt, detailSplit[0], detailSplit[1], false));
            }
                       
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InfoCenter(List<UserInfoCenter> models)
        {

            CheckAda();
            string userId = GetLoggedInUser();
            var db = new ImageSharingDB();
            CloudQueue msgQueue = StorageQueueConnector.CreateQueuesIfNotCreated(userId);
            foreach(UserInfoCenter model in models){
                if (model.Checked)
                {
                    msgQueue.DeleteMessage(model.Id, model.PopReceipt);
                }
            
            }
           // List<UserInfoCenter> model = new List<UserInfoCenter>();
            //foreach (CloudQueueMessage msg in msgQueue.GetMessages(10))
            //{

            //    if (imod.Checked)
            //    {/* 
            //      * Approve the Image
            //      */
            //        image.Approved = true;
            //        // model.Remove(imod);
            //    }
            //    imod.Name = image.Caption;
            //}

            //db.SaveChanges();
            //ViewBag.Message = "Images Approved.";

            return RedirectToAction("Index","Home");

        }



    }
}