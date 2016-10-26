using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers
{
    [Authorize]
    public class RegisterController : BaseController
    {
        //
        // GET: /Register/

        public ActionResult Index()
        {
            CommonWealEntities1 db = new CommonWealEntities1();
            var ngopost = db.NGOPosts.OrderByDescending(x => x.PostDateTime).ToList();


            var s = (from p in db.NGOPosts
                     join u in db.Users on p.EmailID equals u.LoginEmailID
                     into up
                     select up).ToList();





            return View(ngopost);
        }
        //      public ActionResult FileUpload(HttpPostedFileBase file)
        //    {

        //        if (file != null)
        //        {
        //            imagesEntities db = new imagesEntities();
        //            string ImageName = System.IO.Path.GetFileName(file.FileName);
        //            string physicalPath = Server.MapPath("~/images/" + ImageName);

        //            // save image in folder
        //            file.SaveAs(physicalPath);

        //            //save new record in database
        //            tbl_images newRecord = new tbl_images();

        //            newRecord.img_path = ImageName;
        //            db.tbl_images.Add(newRecord);
        //            db.SaveChanges();

        //        }
        //        //Display records
        //        return View("Index");
        //    }
        //    public ActionResult Display(String image)
        //    {


        //        return View();
        //    }

        //}
        [HttpPost]
        public ActionResult PostImage(HttpPostedFileBase file, NGOPost obpost)
        {
            CommonWealEntities1 db = new CommonWealEntities1();
            if (file != null)
            {


                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("/Images/Post/" + ImageName);

                // save image in folder
                file.SaveAs(physicalPath);

                //save new record in database


                obpost.PostUrl = "/Images/Post/" + ImageName;
                obpost.PostType = "Image";
                obpost.PostDateTime = DateTime.Now;
                obpost.ModifiedOn = DateTime.Now;
                obpost.CreatedOn = DateTime.Now;

                obpost.EmailID = this.User.Identity.Name;


                db.NGOPosts.Add(obpost);
                db.SaveChanges();

            }
            else if (obpost.PostContent != null)
            {

                obpost.PostType = "Text";
                obpost.PostDateTime = DateTime.Now;
                obpost.ModifiedOn = DateTime.Now;
                obpost.CreatedOn = DateTime.Now;
                db.NGOPosts.Add(obpost);
                db.SaveChanges();


            }

            return RedirectToAction("Index", "Register");





        }
        //[HttpPost]
        //public JsonResult test(Test ob)
        //{



        //}


        public ActionResult LogOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");

        }
    }
}
