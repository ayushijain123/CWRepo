using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/ Changed By Pooja

        public ActionResult Index()
        {
            CommonWealEntities1 db = new CommonWealEntities1();
            var ngopost = db.NGOPosts.OrderByDescending(x => x.PostDateTime).ToList();

            return View(ngopost);
        }
        [HttpPost]
        public ActionResult PostImage(HttpPostedFileBase file, NGOPost obpost)
        {

            CommonWealEntities1 db = new CommonWealEntities1();
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

            db.NGOPosts.Add(obpost);
            db.SaveChanges();


            return RedirectToAction("Index", "Register");

        }
        public ActionResult Setting()
        {
            return View();
        }
    }
}
