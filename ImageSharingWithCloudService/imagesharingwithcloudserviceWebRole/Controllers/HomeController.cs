using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading;
using imagesharingwithcloudserviceWebRole.Controllers;
using imagesharingwithcloudserviceWebRole.Models;

namespace imagesharingwithcloudserviceWebRole.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {

        public ActionResult Index(String id = "Stranger")
        {
            CheckAda();
            ViewBag.Title = "Welcome";
            String user = GetLoggedInUser();
            if (user == null)
            {
                ViewBag.Id = id;
            }
            else
            {
                ViewBag.Id = user;
            }
            return View();
        }

        public ActionResult Error(String errid = "Unspecified")
        {
            CheckAda();
            if ("Details".Equals(errid))
            {
                ViewBag.Message = "Problem with Details action!";
            }
            else if (errid == "chkImg")
            {
                ViewBag.Message = "file is not an jpag image or larger than 1M";
            }
            else if (errid == "ioFail")
            {
                ViewBag.Message = "fail to upload file";
            }
            else
            {
                ViewBag.Message = errid;
            }
            return View();
        }
    }
}
