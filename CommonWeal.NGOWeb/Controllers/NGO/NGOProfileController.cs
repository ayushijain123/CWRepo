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
    [AllowAnonymous]
    public class NGOProfileController : BaseController
    {
        public ActionResult Index(int id=0)
        {
            try
            {

           
            CommonWealEntities context = new CommonWealEntities();
            dbOperations db = new dbOperations();

            NGOProfilePostCustom NGpost = new NGOProfilePostCustom();
            NGpost.PostWithtopNgoModel = new PostWithTopNgo();
            //User UL = new User();
            //  var userId=context.Users.Where(w=>w.LoginID==LoginUser.LoginID).FirstOrDefault().LoginID;          
            if (id == 0 )
            {
                NGpost.CurrentUserID = LoginUser.LoginID;
                var postList = db.GetPostById(LoginUser.LoginID);
                var image = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault().NGOProfilePic;
               NGpost.imageurl = image;

               NGpost.PostWithtopNgoModel.post = postList;
               NGpost.PostWithtopNgoModel.ngouser = GetTopNgo;
                return View(NGpost);
            }
            else
            {
                NGpost.CurrentUserID = id;
                var postList = db.GetPostById(id);
                var image = context.NGOUsers.Where(x => x.LoginID == id).FirstOrDefault().NGOProfilePic;

                NGpost.imageurl = image;
                NGpost.PostWithtopNgoModel.post = postList;
               
                NGpost.PostWithtopNgoModel.ngouser = GetTopNgo;
                return View(NGpost);
            }
            }
            catch (Exception ex)
            {

                throw ex;
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
           
        }

        [Authorize]
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
        [Authorize]
        [HttpPost]
        public JsonResult Edit(NGOUser obj)
        {bool result1=false;
            using (CommonWealEntities context = new CommonWealEntities())
            {
                if(obj!=null)
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
                result1=true;

                }
            }

            return Json(result1, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [HttpPost]
        public JsonResult PostImage()
        {
            CommonWealEntities context = new CommonWealEntities();
            List<string> imginfo = new List<string>();
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                      var  physicalPath = Server.MapPath("/Images/Post/" + fname);
                        file.SaveAs(physicalPath);
                        var objngo = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
                        objngo.NGOProfilePic = "/Images/Post/" + fname;

                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();

                        imginfo.Add("/Images/Post/"+fname);

                    }
                    // Returns message that successfully uploaded
                    imginfo.Add("File Uploaded Successfully!");
                    return Json(imginfo,JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    imginfo.Add("");
                    imginfo.Add("Error occurred. Error details: " + ex.Message);
                    return Json(imginfo,JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                imginfo.Add("");
                imginfo.Add("No files selected.");
                return Json(imginfo,JsonRequestBehavior.AllowGet);
            }  
            
          
            
        }

        [AllowAnonymous]
        [HttpPost]
        public PartialViewResult AboutUsPartial(int id=0)
        {
            try
            {
                CommonWealEntities context = new CommonWealEntities();
                NGOProfileCustom NgoInfo = new NGOProfileCustom();
                if (id == 0)
                {
                    NgoInfo.WatchNgoID = LoginUser.LoginID;
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
                    NgoInfo.WatchNgoID = id;
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
                NGpostlist.PostWithtopNgoModel = new PostWithTopNgo();
                dbOperations db = new dbOperations();
                //User UL = new User();
                //  var userId=context.Users.Where(w=>w.LoginID==LoginUser.LoginID).FirstOrDefault().LoginID;
                if (id == 0)
                {
                    var obj = db.GetPostById(LoginUser.LoginID);
                    NGpostlist.PostWithtopNgoModel.post = obj;
                    NGpostlist.CurrentUserID = LoginUser.LoginID;
                    //return PartialView("AboutUsNGO",obj);

                    /*returing list to  partial view and than partial view is retuned to ajax call  */
                    return PartialView("~/views/NGOProfile/_NGOProfilePost.cshtml", obj);
                }
                else
                {
                    var obj = db.GetPostById(id);
                    NGpostlist.PostWithtopNgoModel.post = obj;
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



