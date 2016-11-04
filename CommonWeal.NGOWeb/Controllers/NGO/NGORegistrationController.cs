using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonWeal.Data;

namespace CommonWeal.NGOWeb.Controllers.NGO
{
    [AllowAnonymous]
    public class ngoRegistrationController : BaseController
    {
        //
        // GET: /ngoRegistration/
        public ActionResult CreateNGO()
        {

            return View();
        }
        [HttpPost]
        public JsonResult doesEmailExist(string UserName)
        {

            var user = Membership.GetUser(UserName);

            return Json(user == null);
        }

        [HttpPost]
        public ActionResult CreateNGO(NGOUser objngo, HttpPostedFileBase chairmanID, HttpPostedFileBase RegistrationProof)
        {
            CommonWealEntities context = new CommonWealEntities();

            if (ModelState.IsValid)
            {
                if (chairmanID != null)
                {
                    string pic = System.IO.Path.GetFileName(chairmanID.FileName);
                    string copyofregpath = System.IO.Path.Combine(Server.MapPath(@"/Album/ChairmanID/"), pic);
                    // file is uploaded
                    chairmanID.SaveAs(copyofregpath);
                    //save in model property
                    objngo.ChairmanID = "/Album/ChairmanID/" + pic;
                    objngo.ChairmanID = objngo.ChairmanID;
                }
                if (RegistrationProof != null)
                {
                    string pic = System.IO.Path.GetFileName(RegistrationProof.FileName);
                    string copyofregpath = System.IO.Path.Combine(Server.MapPath(@"/Album/RegistrationID/"), pic);
                    // file is uploaded
                    chairmanID.SaveAs(copyofregpath);
                    //save in model property
                    objngo.RegistrationProof = "/Album/RegistrationID/" + pic;
                    objngo.RegistrationProof = objngo.RegistrationProof;
                }
                User obj = new User();
                obj.LoginPassword = objngo.NGOPassword;
                obj.LoginEmailID = objngo.NGOEmailID;
                var roleobj = context.RoleTypes.Where(w => w.RoleName == "NGO").FirstOrDefault();
                obj.LoginUserType = roleobj.RoleID;
                obj.IsActive = false;
                obj.IsBlock = false;
                obj.ModifiedOn = DateTime.Now;
                obj.CreatedOn = DateTime.Now;
                context.Users.Add(obj);
                context.SaveChanges();
                objngo.LoginID = obj.LoginID;
                objngo.IsActive = false;
                objngo.IsBlock = false;
                context.NGOUsers.Add(objngo);
                context.SaveChanges();
                TempData["msg"] = "<script>alert('Change succesfully');</script>";
                //return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
                return RedirectToAction("Index", "Login");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            //return RedirectToAction("CreateNgo", "NgoRegistration");
            return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
        }


        public JsonResult checkEmail(string NGOEmailID)
        {
            CommonWealEntities context = new CommonWealEntities();
            return Json(!context.Users.Any(x => x.LoginEmailID == NGOEmailID), JsonRequestBehavior.AllowGet);
        }

    }
}
