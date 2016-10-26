using CommonWeal.NGOWeb;
using CommonWeal.NGOWeb.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers
{
    [Authorize]
    public class ngoRegistrationController : Controller
    {
        //
        // GET: /ngoRegistration/
        public ActionResult CreateNGO()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateNGO(NGOUserMeta objngoMeta, NGOUser objngo, HttpPostedFileBase chairmanID, HttpPostedFileBase RegistrationProof)
        {
            CommonWealEntities1 context = new CommonWealEntities1();

            if (ModelState.IsValid)
            {
                if (chairmanID != null)
                {
                    string pic = System.IO.Path.GetFileName(chairmanID.FileName);
                    string copyofregpath = System.IO.Path.Combine(Server.MapPath(@"/Album/ChairmanID/"), pic);
                    // file is uploaded
                    chairmanID.SaveAs(copyofregpath);
                    //save in model property
                    objngoMeta.ChairmanID = "/Album/ChairmanID/" + pic;
                    objngo.ChairmanID = objngoMeta.ChairmanID;
                }
                if (RegistrationProof != null)
                {
                    string pic = System.IO.Path.GetFileName(RegistrationProof.FileName);
                    string copyofregpath = System.IO.Path.Combine(Server.MapPath(@"/Album/RegistrationID/"), pic);
                    // file is uploaded
                    chairmanID.SaveAs(copyofregpath);
                    //save in model property
                    objngoMeta.RegistrationProof = "/Album/RegistrationID/" + pic;
                    objngo.RegistrationProof = objngoMeta.RegistrationProof;
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
                TempData["msg"] = ("<script>alert('Registered successfully');</script>");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction("CreateNgo", "NgoRegistration");
        }
    }
}
