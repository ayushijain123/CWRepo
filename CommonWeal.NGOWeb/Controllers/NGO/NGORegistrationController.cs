using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        public class ImageHandler
        {
            public byte[] ImagePost(HttpPostedFileBase file)
            {
                byte[] fileData = null;

                if (file != null)
                {
                    // var docfiles = new List<string>();
                    //byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(file.ContentLength);
                    }
                }
                return fileData;
            }
        }
        [HttpPost]
        public ActionResult CreateNGO(NGOUser objngo, HttpPostedFileBase chairmanID, HttpPostedFileBase RegistrationProof)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (chairmanID != null && RegistrationProof != null)
                    {
                        ImageHandler img = new ImageHandler();
                        var res = img.ImagePost(chairmanID);
                        var res1 = img.ImagePost(RegistrationProof);
                       // string result = System.Text.Encoding.UTF8.GetString(byteArray);                  
                        objngo.ChairmanID = Convert.ToBase64String(res);
                        objngo.RegistrationProof = Convert.ToBase64String(res1);
                    }
                    var result = Task.Run(() => APIHelper<string>.PostJson("NGORegistration/CreateNGO", objngo));
                  
                   // return JavaScript("window.location = '" + Url.Action("Index", "Welcome") + "'");
                    return RedirectToAction("Index", "Welcome");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            /*the form type is ajaxform  that's why return type should be (return JavaScript) */
            return JavaScript("window.location = '" + Url.Action("Index", "Login") + "'");
        }

        /*to identify email address uniquely through ajax on registration*/
        public JsonResult checkEmail(string UserEmail)
        {
            CommonWealEntities context = new CommonWealEntities();
            return Json(!context.Users.Any(x => x.LoginEmailID == UserEmail), JsonRequestBehavior.AllowGet);
        }


    }
}
