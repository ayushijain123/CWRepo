using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;

namespace CommonWeal.NGOWeb.Controllers.NGO
{
    [Authorize(Roles="NGOAdmin")]
    public class NGOHomeController :BaseController
    {
        //
        // GET: /NGOHome/
       
       
        public ActionResult Index()
        {
            dbOperations ob = new dbOperations();
            var postlist = ob.GetAllPost();
            postlist= postlist.OrderByDescending(x => x.postCreateTime).ToList();
            return View(postlist);
        }


        [HttpPost]
        public ActionResult PostImage(HttpPostedFileBase file, NGOPost obpost)
        {
            CommonWealEntities db = new CommonWealEntities();
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
                obpost.EmailID = this.User.Identity.Name;
                obpost.PostType = "Text";
                obpost.PostDateTime = DateTime.Now;
                obpost.ModifiedOn = DateTime.Now;
                obpost.CreatedOn = DateTime.Now;
                db.NGOPosts.Add(obpost);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "NGOHome");
        }
       

    }
}
