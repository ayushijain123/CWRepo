using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        public ActionResult Index()
        {
            return View();
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

    }
}
