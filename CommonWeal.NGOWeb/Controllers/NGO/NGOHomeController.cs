using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;
using Newtonsoft.Json;
using CommonWeal.NGOWeb.ViewModel;
namespace CommonWeal.NGOWeb.Controllers.NGO
{   /*entry for authorized user and whose role is NGOAdmin*/
    [Authorize(Roles = "NGOAdmin")]
    public class NGOHomeController : BaseController
    {
        //
        // GET: /NGOHome/


        public ActionResult Index()
        {
            

            
            dbOperations ob = new dbOperations();

            /*get postList from GetAllPost method*/
            var postlist = ob.GetPostOnLoad();
            postlist = postlist.OrderByDescending(x => x.postCreateTime).ToList();
            var list = ob.GetAllCategory();
           // ViewBag.categoryList = new SelectList(list,"categoryId","categoryName");
            /*return To view with postList */
            PostWithTopNgo pwtn = new PostWithTopNgo();
            pwtn.post = postlist;
            pwtn.ngouser = GetTopNgo;
            return View(pwtn);
        }

        /*method for upload image*/
        [HttpPost]
        public ActionResult PostImage(HttpPostedFileBase file, NGOPost obpost,int[] category)
        {
            dbOperations ob = new dbOperations();
            CommonWealEntities db = new CommonWealEntities();
            /*if file is uploaded with or without content message*/
            if (file != null)
            {
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("/Images/Post/" + ImageName);
                // save image in folder
                file.SaveAs(physicalPath);
                //save new record in database
                obpost.PostUrl = "/Images/Post/" + ImageName;
                obpost.PostType = "Image";
                obpost.PostDateTime = System.DateTime.Now;
                obpost.ModifiedOn = System.DateTime.Now;
                obpost.CreatedOn = System.DateTime.Now;
                obpost.PostCommentCount = 0;
                obpost.PostLikeCount = 0;
                /*login user login id is a property in base controller
                 which contain current user information from user table*/
                obpost.LoginID = LoginUser.LoginID;
                    //this.User.Identity.Name;
                db.NGOPosts.Add(obpost);
                db.SaveChanges();
                ob.SubmitPostCategory(obpost.PostID, category);
                
            }
                /*if only content is posted without any image*/
            else if (obpost.PostContent != null)
            {
                /*login user login id is a property in base controller
                which contain current user information from user table*/
                obpost.LoginID=LoginUser.LoginID; 
                    //this.User.Identity.Name;
                obpost.PostType = "Text";
                obpost.PostDateTime = DateTime.Now;
                obpost.ModifiedOn = DateTime.Now;
                obpost.CreatedOn = DateTime.Now;
                obpost.PostCommentCount = 0;
                obpost.PostLikeCount = 0;
                db.NGOPosts.Add(obpost);
                db.SaveChanges();
                ob.SubmitPostCategory(obpost.PostID, category);
            }
           Console.Write("<script>alert('"+obpost.PostID+"')</script>");
            /*return to current view*/
            return RedirectToAction("Index", "NGOHome");
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
