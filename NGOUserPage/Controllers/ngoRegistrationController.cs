using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NGOUserPage.CommonOperation;

namespace NGOUserPage.Controllers
{
    public class ngoRegistrationController : Controller
    {
        //
        // GET: /ngoRegistration/
        

        dbOperation db = new dbOperation();

        public ActionResult Index()
        {
            var oa = db.getList();
            ViewBag.Areaofinterest = oa;
           var qa = db.GetArea();
           ViewBag.value = qa;

           List<SelectListItem> li = new List<SelectListItem>();
           li.Add(new SelectListItem { Text = "Select", Value = "0" });
           li.Add(new SelectListItem { Text = "India", Value = "1" });
           li.Add(new SelectListItem { Text = "Srilanka", Value = "2" });
           li.Add(new SelectListItem { Text = "China", Value = "3" });
           li.Add(new SelectListItem { Text = "Austrila", Value = "4" });
           li.Add(new SelectListItem { Text = "USA", Value = "5" });
           li.Add(new SelectListItem { Text = "UK", Value = "6" });
           SelectList sl = new SelectList(li,"Value","Text");
           ViewBag.Country = sl;
            return View();
        }

        [HttpPost]
        public ActionResult Index(tbl_ngo objngo, HttpPostedFileBase filengo_CopyOfRegistration, HttpPostedFileBase filengo_PhotoId)
        {
            if (filengo_CopyOfRegistration != null)
            {
                string pic = System.IO.Path.GetFileName(filengo_CopyOfRegistration.FileName);
                int position = pic.IndexOf(".");
                int length=pic.Length-position;

                if ((pic.Substring(position, length)).ToLower().Equals(".jpg") || (pic.Substring(position, length)).ToLower().Equals(".png"))
                {
                    string copyofregpath = System.IO.Path.Combine(Server.MapPath(@"/NgoImage/"), pic);
                    // file is uploaded
                    filengo_CopyOfRegistration.SaveAs(copyofregpath);
                    //save in model property
                    objngo.ngo_CopyOfRegistration = "NgoImage/" + pic;


                }
                else {
                    Response.Write("<script>alert('uploaded image should be in the format of .jpg and .png ')</script>");
                    return View();
                
                }
                
            }
            if (filengo_PhotoId != null)
            {
                string pic = System.IO.Path.GetFileName(filengo_PhotoId.FileName);
                int position = pic.IndexOf(".");
                int length = pic.Length - position;
                if ((pic.Substring(position, length)).ToLower().Equals(".jpg") || (pic.Substring(position, length)).ToLower().Equals(".png"))
                {

                    string photoidpath = System.IO.Path.Combine(Server.MapPath(@"/NgoImage/"), pic);
                    // file is uploaded
                    filengo_PhotoId.SaveAs(photoidpath);

                    //save in model property
                    objngo.ngo_PhotoId = "NgoImage/" + pic;



                }
                    
                else {

                    Response.Write("<script>alert('uploaded image should be in the format of .jpg and .png ')</script>");
                    return View();
                
                }
                return RedirectToAction("Display","ngoRegistration");
               
            }
            tbl_login loginObj = new tbl_login();
            //loginObj.Login_mobileNumber = objngo.ngo_UserId.ToString();
            objngo.ngo_Type = "NGO";
            loginObj.login_userType = "NGO";
            loginObj.isAccepted = 0;
            loginObj.login_Email = objngo.ngo_Email;
            loginObj.login_password = objngo.ngo_password;
            long id = db.Insert(loginObj);
            objngo.login_id = id;
            var result1 = db.Insert(objngo);

            if (result1 && id != 0)
            {
                EmailGateway objemail = new EmailGateway();
                string mailto = "vilas.holkar@valuelabs.com";
                string subject = "new ngorequest : " + objngo.ngo_userName;
                string message = "please Login into your account and verify ngo request whose email id is:" + objngo.ngo_Email + " and name is: " + objngo.ngo_userName;
                bool emailresult = objemail.sendEmail(mailto, subject, "", message);
                if (emailresult)
                {
                    Response.Write("<script>alert('please wait for email through admin that will verify your ngoid ')</script>");
                }

            }
            
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            li.Add(new SelectListItem { Text = "India", Value = "1" });
            li.Add(new SelectListItem { Text = "Srilanka", Value = "2" });
            li.Add(new SelectListItem { Text = "China", Value = "3" });
            li.Add(new SelectListItem { Text = "Austrila", Value = "4" });
            li.Add(new SelectListItem { Text = "USA", Value = "5" });
            li.Add(new SelectListItem { Text = "UK", Value = "6" });
            SelectList sl = new SelectList(li, "Value", "Text");
            ViewBag.Country = sl;
            return View();

        }

        public ActionResult Display() { 
        
                    return View();
        
        }


    }
}
