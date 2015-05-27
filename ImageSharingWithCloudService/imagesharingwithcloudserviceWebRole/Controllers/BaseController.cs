using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WebMatrix.Data;
using WebMatrix.WebData;
using imagesharingwithcloudserviceWebRole.DAL;
using imagesharingwithcloudserviceWebRole.Models;

namespace imagesharingwithcloudserviceWebRole.Controllers
{
    public class BaseController : Controller
    {
        protected void CheckAda()
        {
            HttpCookie cookie = Request.Cookies.Get("ImageSharing");
            if (cookie != null)
            {
                if ("true".Equals(cookie["ADA"]))
                    ViewBag.isAda = true;
                else
                    ViewBag.isAda = false;

            }
            else { ViewBag.isAda = false; }

        }
        protected void SaveCookie(bool ADA)
        {
            HttpCookie cookie = new HttpCookie("ImageSharing");
            cookie.Expires = DateTime.Now.AddMonths(3);
            cookie.HttpOnly = true;
            //cookie["Userid"] = userid;
            cookie["ADA"] = ADA ? "true" : "false";
            Response.Cookies.Add(cookie);
        }

        protected String GetLoggedInUser()
        {
            string name = WebSecurity.CurrentUserName;
            return "".Equals(name) ? null : name;
        }

        protected IEnumerable<Image> ApprovedImages(IEnumerable<Image> images)
        {
            return images.Where(img => img.Approved);
        }
        protected IEnumerable<Image> ApprovedImages()
        {
            var db = new ImageSharingDB();
            return ApprovedImages(db.Images);
        }
        //protected String GetLoggedInUser()
        //{
        //    HttpCookie cookie = Request.Cookies.Get("ImageSharing");
        //    if (cookie != null && cookie["Userid"] != null)
        //    {
        //        return cookie["Userid"];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //protected ActionResult ForceLogin()
        //{
        //    return RedirectToAction("Login", "Account");
        //}

    }
}