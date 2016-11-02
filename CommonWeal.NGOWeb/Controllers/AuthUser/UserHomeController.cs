using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers.AuthUser
{
    [Authorize]
    public class UserHomeController : BaseController
    {
        public ActionResult Index()
        {
            dbOperations ob = new dbOperations();
            var postlist=ob.GetAllPost();
            CommonWealEntities1 db = new CommonWealEntities1();
            var ngopost = db.NGOPosts.OrderByDescending(x => x.PostDateTime).ToList();
            var s = (from p in db.NGOPosts
                     join u in db.Users on p.EmailID equals u.LoginEmailID
                     into up
                     select up).ToList();
           postlist= postlist.OrderByDescending(x => x.postCreateTime).ToList();
            return View(postlist);
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
        public ActionResult UserPostComment(string strComment, int postId)
        {
            CommonWealEntities1 db = new CommonWealEntities1();
            PostComment postcmnt = new PostComment();
            postcmnt.CommentText = strComment;
            postcmnt.CreatedOn = DateTime.Now;
            postcmnt.ModifiedOn = DateTime.Now;
            postcmnt.PostID = postId;
            postcmnt.UserID = User.Identity.Name;

            db.PostComments.Add(postcmnt);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

       
    }
}
