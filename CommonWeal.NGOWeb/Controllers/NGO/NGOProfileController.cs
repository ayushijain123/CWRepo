using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;


namespace CommonWeal.NGOWeb.Controllers.NGO
{
    //[Authorize]
    [Authorize(Roles = "NGOAdmin")]
    public class NGOProfileController : BaseController
    {
        public ActionResult Index()
        {
            CommonWealEntities context = new CommonWealEntities();
            dbOperations db = new dbOperations();
            //User UL = new User();
          //  var userId=context.Users.Where(w=>w.LoginID==LoginUser.LoginID).FirstOrDefault().LoginID;
           var postList= db.GetAllPost();
           var ngoPostList = postList.Where(w => w.userId == LoginUser.LoginID).OrderByDescending(x=>x.postCreateTime).ToList();
            return View(ngoPostList);
        }

         /*action for getting ngo details*/
        public ActionResult AboutUs()
        {
            int res = LoginUser.LoginID;
            dbOperations obj = new dbOperations();
            var details = obj.GetNGODetails(res);
            if (details != null)
            {
                // ViewBag.value = details;
                return View(details);
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Album()
        {
            return View();
        }
    }
}
