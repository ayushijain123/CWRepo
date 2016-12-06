using CommonWeal.Data;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;
using Newtonsoft.Json;

namespace CommonWeal.NGOWeb.Controllers.NGO
{
    //[Authorize]
    [Authorize(Roles = "NGOAdmin")]
    public class NGOProfileController : BaseController
    {
        public ActionResult Index()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations db = new dbOperations();
            //User UL = new User();
            //  var userId=context.Users.Where(w=>w.LoginID==LoginUser.LoginID).FirstOrDefault().LoginID;
            var postList = db.GetPostById(LoginUser.LoginID);
            var image= context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault().NGOProfilePic;
            ViewBag.imageurl=image;
            //var ngoPostList = context.Where(w => w.userId == LoginUser.LoginID).OrderByDescending(x => x.postCreateTime).ToList();
            return View(postList);
        }

        /*action for getting ngo details*/
        [HttpGet]
        public ActionResult AboutUs()
        {
            CommonWealEntities context = new CommonWealEntities();

            var obj = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(NGOUser obj)
        {
            using (CommonWealEntities context = new CommonWealEntities())
            {
                var ob = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
                ob.NGOEmailID = obj.NGOEmailID;
                ob.Mobile = obj.Mobile;
                ob.NGOAddress = obj.NGOAddress;
                ob.NGOName = obj.NGOName;
                ob.Telephone = obj.Telephone;
                ob.City = obj.City;
                ob.ChairmanName = obj.ChairmanName;
                //ob.NGOPassword = obj.NGOPassword;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                var ob1 = context.Users.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
                ob1.LoginEmailID = obj.NGOEmailID;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                TempData["US"] = "<script>alert('Updated successfully!');</script>";
            }

            return RedirectToAction("AboutUs", "NGOProfile");
        }
        [HttpPost]
        public ActionResult PostImage(HttpPostedFileBase file, NGOUser obj)
        {
            CommonWealEntities context = new CommonWealEntities();
            if (file != null)
            {
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("/Images/Post/" + ImageName);
                // save image in folder
                file.SaveAs(physicalPath);
                var objngo = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
                objngo.NGOProfilePic = "/Images/Post/" + ImageName;
                
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();


            }
            return RedirectToAction("Index", "NgoProfile");
        }
    }
}