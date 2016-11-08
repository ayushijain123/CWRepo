using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonWeal.Data;
using System.Data.Entity.Validation;

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
            try
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
                    return RedirectToAction("Index", "Welcome");
                }
                if (!ModelState.IsValid)
                {
                    return View();
                }
                return RedirectToAction("CreateNgo", "NgoRegistration");
                //return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }


        public JsonResult checkEmail(string NGOEmailID)
        {
            CommonWealEntities context = new CommonWealEntities();
            return Json(!context.Users.Any(x => x.LoginEmailID == NGOEmailID), JsonRequestBehavior.AllowGet);
        }

    }
}
