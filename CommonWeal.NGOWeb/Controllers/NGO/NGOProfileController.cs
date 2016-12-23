using CommonWeal.Data;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Web;
using CommonWeal.NGOWeb.Utility;
using Newtonsoft.Json;
using System.Web.Mvc.Ajax;
using CommonWeal.NGOWeb.ViewModel;
namespace CommonWeal.NGOWeb.Controllers.NGO
{
    //[AuthorizeRoles = "NGOAdmin")]]
    [Authorize]
    public class NGOProfileController : BaseController
    {
        public ActionResult Index(int id=0)
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations db = new dbOperations();

            NGOProfilePostCustom NGpost = new NGOProfilePostCustom();
            //User UL = new User();
            //  var userId=context.Users.Where(w=>w.LoginID==LoginUser.LoginID).FirstOrDefault().LoginID;           
            if (id == 0 )
            {
                NGpost.CurrentUserID = LoginUser.LoginID;
                var postList = db.GetPostById(LoginUser.LoginID);
                var image = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault().NGOProfilePic;
               NGpost.imageurl = image;
               NGpost.PostModel = postList;
                return View(NGpost);
            }
            else
            {
                NGpost.CurrentUserID = id;
                var postList = db.GetPostById(id);
                var image = context.NGOUsers.Where(x => x.LoginID == id).FirstOrDefault().NGOProfilePic;
                NGpost.imageurl = image;
                NGpost.PostModel = postList;
                return View(NGpost);
            }
            //var ngoPostList = context.Where(w => w.userId == LoginUser.LoginID).OrderByDescending(x => x.postCreateTime).ToList();
         
        }
        public ActionResult Post()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations db = new dbOperations();
            //User UL = new User();
            //  var userId=context.Users.Where(w=>w.LoginID==LoginUser.LoginID).FirstOrDefault().LoginID;
            var postList = db.GetPostById(LoginUser.LoginID);
            var image = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault().NGOProfilePic;
            ViewBag.imageurl = image;
            //var ngoPostList = context.Where(w => w.userId == LoginUser.LoginID).OrderByDescending(x => x.postCreateTime).ToList();
            return View(postList);
        }

        /*action for getting ngo details*/
        [HttpGet]
        public ActionResult AboutUs()
        {
           // LoginUser.LoginID = id;
            CommonWealEntities context = new CommonWealEntities();
            var obj = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
            return PartialView("AboutUsNGO", obj);
           // return View(obj);
            //return PartialView("AboutUsNGO",obj);
            return View(obj);
        }

        [HttpPost]
        public JsonResult SubmitSummary(string summary, int Loginid)
        {
            string summery;
            CommonWealEntities context = new CommonWealEntities();
            try
            {
                var obj = context.NGOUsers.Where(x => x.LoginID == Loginid).FirstOrDefault();
                obj.AboutUs = summary;
                context.Configuration.ValidateOnSaveEnabled = false;

                context.SaveChanges();
                summary = obj.AboutUs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(summary, JsonRequestBehavior.AllowGet);

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

            return RedirectToAction("Index", "NGOProfile");//, new AjaxOptions { UpdateTargetId = "lblpost" }
            //CommonWealEntities mycontext = new CommonWealEntities();
            //var myobj = mycontext.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
            //return PartialView("AboutUsNGO", myobj);

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
            return RedirectToAction("Index", "NGOProfile");
        }


        [AllowAnonymous]
        public PartialViewResult AboutUsPartial(int id=0)
        {
            try
            {
                CommonWealEntities context = new CommonWealEntities();
                NGOProfileCustom NgoInfo = new NGOProfileCustom();
                if (id == 0)
                {
                    NgoInfo.UserID = LoginUser.LoginID;
                    var obj = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
                    NgoInfo.NgoUser = obj;
                    var image = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault().NGOProfilePic;
                    NgoInfo.imageurl = image;
                    //return PartialView("AboutUsNGO",obj);

                    /*returing list to  partial view and than partial view is retuned to ajax call  */
                    return PartialView("~/views/NGOProfile/_AboutUs.cshtml", NgoInfo);
                }
                else
                {
                    NgoInfo.UserID = LoginUser.LoginID;
                    var obj = context.NGOUsers.Where(x => x.LoginID == id).FirstOrDefault();
                    NgoInfo.NgoUser = obj;
                    var image = context.NGOUsers.Where(x => x.LoginID == id).FirstOrDefault().NGOProfilePic;
                    NgoInfo.imageurl = image;
                    return PartialView("~/views/NGOProfile/_AboutUs.cshtml", NgoInfo);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [AllowAnonymous]
        public PartialViewResult NGOProfilePost(int id=0)
        {
            try
            {
                NGOProfilePostCustom NGpostlist = new NGOProfilePostCustom();
                dbOperations db = new dbOperations();
                //User UL = new User();
                //  var userId=context.Users.Where(w=>w.LoginID==LoginUser.LoginID).FirstOrDefault().LoginID;
                if (id == 0)
                {
                    var obj = db.GetPostById(LoginUser.LoginID);
                    NGpostlist.PostModel = obj;
                    NGpostlist.CurrentUserID = LoginUser.LoginID;
                    //return PartialView("AboutUsNGO",obj);

                    /*returing list to  partial view and than partial view is retuned to ajax call  */
                    return PartialView("~/views/NGOProfile/_NGOProfilePost.cshtml", obj);
                }
                else
                {
                    var obj = db.GetPostById(id);
                    NGpostlist.PostModel = obj;
                    NGpostlist.CurrentUserID =id;
                    //return PartialView("AboutUsNGO",obj);

                    /*returing list to  partial view and than partial view is retuned to ajax call  */
                    return PartialView("~/views/NGOProfile/_NGOProfilePost.cshtml", NGpostlist);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}