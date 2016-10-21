using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGOUserPage.Models;
using NGOUserPage.CommonOperation;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.Collections;

namespace NGOUserPage.Controllers
{
    public class ResetController : Controller
    {
        dbOperation ob = new dbOperation();
        commonwealdbEntities db = new commonwealdbEntities();
        // GET: /Reset/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Index(Reset model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyPassword(Reset model)
        {
            // Added by Rishiraj Start
            //TempData["ID"] = email;
            Session["EmailID"] = model.email_address;

            //SHA256 sha = new SHA256CryptoServiceProvider();
            //byte[] dataBytes = Utility.GetBytes(email);
            //byte[] resultBytes = sha.ComputeHash(dataBytes);

            //// return the hash string to the caller
            //return Utility.GetString(resultBytes);

            // Added by Rishiraj End
            var result = ob.CheckEmail(model.email_address);
            string num = "0123456789ASDFGHJMNOL";
            int len = num.Length;
            string otp = string.Empty;
            int otpdigit = 5;
            string finaldigit;
            int getindex;
            for (int i = 0; i < otpdigit; i++)
            {
                do
                {
                    getindex = new Random().Next(0, len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                } while (otp.IndexOf(finaldigit) != -1);
                otp += finaldigit;

            }
            ViewBag.value = otp;
            if (result)
            {
                EmailGateway objemail = new EmailGateway();
                string mailto = model.email_address;
                string subject = "New Password Requested ";
                string message = ViewBag.value;
                bool emailresult = objemail.sendEmail(mailto, subject, "", message);
                
            }
            else
            {

                Response.Write("<script>alert('Your email does not match to our records ')</script>");
                return View("Index");
            }
            return View();
            
        }

        [HttpPost]
        public ActionResult update(String email, String newpwd)
        {
            email = Session["EmailID"].ToString();
            var result1 = ob.updatePwd(email, newpwd);
            if (result1)
            {
                Response.Write("<script>alert('updated ')</script>");
            }
            //db.SaveChanges();
            Session.Remove("EmailID");
            
            return View();

        }
        
      

  
    }
    }

