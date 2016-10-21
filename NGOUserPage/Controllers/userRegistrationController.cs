using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGOUserPage.Models;


namespace NGOUserPage.Controllers
{
    public class userRegistrationController : Controller
    {
        //
        // GET: /userRegistration/
        dbOperation con = new dbOperation();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index1(userregistration usrvalue )
        {
            if (!ModelState.IsValid)
            {
                long id = 0;
                tbl_login loginObj = new tbl_login();
                tbl_user usr=new tbl_user();
                loginObj.login_userType = "USER";
                loginObj.isAccepted = 1;
                loginObj.login_Email = usrvalue.login_Email;
                loginObj.login_password = usrvalue.login_password;

                id = con.Insert(loginObj);
                usr.login_id = id;
                usr.login_userType = "USER";
                usr.login_userName = usrvalue.firstName + " " + usrvalue.lastName;
                usr.login_password = usrvalue.login_password;
                usr.Login_mobileNumber = usrvalue.Login_mobileNumber;
                usr.login_Email = usrvalue.login_Email;
                con.Insert(usr);
                Response.Write("<script>alert('Registration successfully')</script>");
            }
                
             return View();
            }
           



        }

    }

