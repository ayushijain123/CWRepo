using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGOUserPage.Controllers
{
    public class userLoginController : Controller
    {
        //
        // GET: /userLogin/

        dbOperation object1 = new dbOperation();

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string login_password, string login_Email)
        {
            int success = 0;
           

            if (login_password != "" && login_Email != "")
            {
                success = object1.userLogin(login_Email, login_password);

            }

            if (success == 1)
            {
                Session["UserID"] = login_Email;
                return RedirectToAction("Index", "welcome");
            }
            else
            {
                Response.Write("<script>alert('please enter valid credentials ')</script>");
                return View();
            }
        }



    }
}
