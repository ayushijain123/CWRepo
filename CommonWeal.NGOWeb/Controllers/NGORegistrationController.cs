using CommonWeal.NGOWeb;
using CommonWeal.NGOWeb.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers
{
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
                UserLogin obj = new UserLogin();
                obj.LoginPassword = objngo.NGOPassword;
                obj.LoginEmailID = objngo.NGOEmailID;
                var roleobj = context.RoleTypes.Where(w => w.RoleName == "NGO").FirstOrDefault();
                obj.LoginUserType = roleobj.RoleID;
                obj.IsActive = false;
                obj.IsBlock = false;
                obj.ModifiedOn = DateTime.Now;
                obj.CreatedOn = DateTime.Now;
                context.UserLogins.Add(obj);
                context.SaveChanges();
                objngo.LoginID = obj.LoginID;
                context.NGOUsers.Add(objngo);
                context.SaveChanges();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction("CreateNgo", "NgoRegistration");
        }
    }
}
