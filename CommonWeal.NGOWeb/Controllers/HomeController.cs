﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;

namespace CommonWeal.NGOWeb.Controllers
{

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        //
        // GET: /Home/ Changed By Pooja

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {

                dbOperations ob = new dbOperations();
                var postlist = ob.GetAllPost();
                CommonWealEntities db = new CommonWealEntities();
                var ngopost = db.NGOPosts.OrderByDescending(x => x.PostDateTime).ToList();

                var t = UIHelper.GetDropDownListFromEnum(typeof(EnumHelper.UserType));
                postlist = postlist.OrderByDescending(x => x.postCreateTime).ToList();

                return View(postlist);
                //return View();

            }
            else
            {
                if (this.User.IsInRole(EnumHelper.UserType.NGOAdmin.ToString()))
                {
                    return RedirectToAction("Index", "NGOHome");
                }

                else
                {
                    return RedirectToAction("Index", "UserHome");
                }

            }
        }
        //[HttpPost]
        //public ActionResult PostImage(HttpPostedFileBase file, NGOPost obpost)
        //{

        //    CommonWealEntities1 db = new CommonWealEntities1();
        //    string ImageName = System.IO.Path.GetFileName(file.FileName);
        //    string physicalPath = Server.MapPath("/Images/Post/" + ImageName);

        //    // save image in folder
        //    file.SaveAs(physicalPath);

        //    //save new record in database


        //    obpost.PostUrl = "/Images/Post/" + ImageName;
        //    obpost.PostType = "Image";
        //    obpost.PostDateTime = DateTime.Now;
        //    obpost.ModifiedOn = DateTime.Now;
        //    obpost.CreatedOn = DateTime.Now;

        //    db.NGOPosts.Add(obpost);
        //    db.SaveChanges();


        //    return RedirectToAction("Index", "Register");

        //}
        public ActionResult Setting()
        {
            return View();
        }
    }
}
