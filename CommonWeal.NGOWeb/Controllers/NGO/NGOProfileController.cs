using CommonWeal.Data;
using System.Linq;
using System.Web.Mvc;


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
            var postList = db.GetPostOnLoad();
            //var ngoPostList = postList.Where(w => w.userId == LoginUser.LoginID).OrderByDescending(x => x.postCreateTime).ToList();
            return View(postList);
        }

        /*action for getting ngo details*/
        [HttpGet]
        public ActionResult AboutUs()
        {
            CommonWealEntities context = new CommonWealEntities();
            var obj = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(NGOUser obj)
        {
            using (CommonWealEntities context = new CommonWealEntities())
            {
                var ob = context.NGOUsers.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
                ob.NGOEmailID = obj.NGOEmailID;
                ob.Mobile = obj.Mobile;
                ob.NGOAddress = obj.NGOAddress;
                ob.NGOName = obj.NGOName;
                ob.Telephone = obj.Telephone;
                ob.City = obj.City;
                ob.ChairmanName = obj.ChairmanName;
                ob.NGOPassword = obj.NGOPassword;
                context.Configuration.ValidateOnSaveEnabled = false;
                context.SaveChanges();
                User objuser = new User();
                var Userob = context.Users.Where(x => x.LoginID == LoginUser.LoginID).FirstOrDefault();
                Userob.LoginEmailID = obj.NGOEmailID;
                context.SaveChanges();



            }
            TempData["AlertMessage"] = "records updated successfully";
            return RedirectToAction("aboutUs", "NGOProfile");

        }

    }
}
