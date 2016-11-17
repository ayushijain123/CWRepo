using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;
using Newtonsoft.Json;

namespace CommonWeal.NGOWeb.Controllers.AuthUser
{
    [Authorize]
    public class UserHomeController : BaseController
    {
        public ActionResult Index()
        {
            
            /* Sammple API call */
          //  APIHelper helper = new APIHelper();

           // var payLoad = helper.GetJson("/Post/PostAll");

          //  var data = JsonConvert.DeserializeObject<List<PostComment>>(payLoad);
            /* Sammple API call */

            dbOperations ob = new dbOperations();
           /*call method from dboperation class for getting post list*/
            var postlist = ob.GetAllPost();
            CommonWealEntities db = new CommonWealEntities();
            //var ngopost = db.NGOPosts.OrderByDescending(x => x.PostDateTime).ToList();
            //var s = (from p in db.NGOPosts
            //         join u in db.Users on p.EmailID equals u.LoginEmailID
            //         into up
            //         select up).ToList();
            
            postlist = postlist.OrderByDescending(x => x.postCreateTime).ToList();
            /*return to view with post list*/
            return View(postlist);
            //return View();
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


        /*for submit comment by ngo and registered user*/
        [HttpPost]
        public ActionResult UserPostComment(string strComment, int postId)
        {
            CommonWealEntities db = new CommonWealEntities();
            PostComment postcmnt = new PostComment();
            postcmnt.CommentText = strComment;
            postcmnt.CreatedOn = DateTime.Now;
            postcmnt.ModifiedOn = DateTime.Now;
            postcmnt.PostID = postId;
            /*use current username from loginUser viewbag definrd from basecontroller*/
            postcmnt.LoginID = LoginUser.LoginID;
                //Convert.ToInt32(User.Identity.Name);

            db.PostComments.Add(postcmnt);
            db.SaveChanges();
            /* redirect to home than from their it will redirect on accordintg to role*/
            return RedirectToAction("Index", "Home");
        }


    }
}
