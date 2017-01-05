using CommonWeal.Data;
using CommonWeal.NGOWeb.Utility;
using CommonWeal.NGOWeb.ViewModel;
using System.Linq;
using System.Web.Mvc;
namespace CommonWeal.NGOWeb.Controllers
{

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        //
        // GET: /Home/ Changed By Pooja

        public ActionResult Index()
        {

            CommonWealEntities context = new CommonWealEntities();
            PostWithTopNgo pwtn = new PostWithTopNgo();
            var ngolist = context.NGOUsers.ToList();
            var ngopostlist = context.NGOPosts.ToList();

            foreach (var obj in ngopostlist)
            {

                CommonWealEntities context1 = new CommonWealEntities();

                int likecount = context1.PostLikes.Where(x => x.PostID == obj.PostID).Count();
                int commentcount = context1.PostComments.Where(x => x.PostID== obj.PostID).Count();

                obj.PostCommentCount = commentcount;
                obj.PostLikeCount = likecount;

                context1.Configuration.ValidateOnSaveEnabled = false;

                context1.SaveChanges();

            }

            foreach (var ob in ngolist)
            {
                CommonWealEntities context1 = new CommonWealEntities();
                var postList = context1.NGOPosts.Where(x => x.LoginID == ob.LoginID).ToList();
                var total = 0;

                foreach (var item in postList)
                {
                    total = total + item.PostCommentCount.Value + item.PostLikeCount.Value;
                }
                ob.PostCount = total;
                context1.Configuration.ValidateOnSaveEnabled = false;

                context1.SaveChanges();
            }
            //PostWithTopNgo pwtn = new PostWithTopNgo();
            //var ngolist = context.NGOUsers.ToList();
            //var ngopostlist = context.NGOPosts.ToList();
            //foreach (var obj in ngolist)
            //{



            //    int count = ngopostlist.Where(x => x.LoginID == obj.LoginID).Count();

            //    obj.PostCount = count;
            //    context.Configuration.ValidateOnSaveEnabled = false;

            //    context.SaveChanges();

            //}

            if (!User.Identity.IsAuthenticated || this.User.IsInRole("Admin"))
            {

                dbOperations ob = new dbOperations();
                var postlist = ob.GetPostOnLoad();
                CommonWealEntities db = new CommonWealEntities();
                var ngopost = db.NGOPosts.OrderByDescending(x => x.PostDateTime).ToList();

                var t = UIHelper.GetDropDownListFromEnum(typeof(EnumHelper.UserType));
                postlist = postlist.OrderByDescending(x => x.postCreateTime).ToList();
                pwtn.post = postlist;
                pwtn.ngouser = GetTopNgo;
                return View(pwtn);
                //return View();

            }
            else
            {

                if (this.User.IsInRole(EnumHelper.UserType.NGOAdmin.ToString()))
                {
                    return RedirectToAction("Index", "NGOHome");
                }

                else if (this.User.IsInRole(EnumHelper.UserType.Admin.ToString()))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "UserHome");
                }

            }
        }
        //[HttpPost]
        //public ActionResult PostImage(HttpPostedFileBase file, NGOPost obpost)
        //{

        //    CommonWealEntities1 db = new CommonWealEntities1();
        //    string ImageName = System.IO.Path.GetFileName(file.FileName);
        //    string physicalPath = Server.MapPath("/Images/Post/" + ImageName);

        //    // save image in folder
        //    file.SaveAs(physicalPath);

        //    //save new record in database


        //    obpost.PostUrl = "/Images/Post/" + ImageName;
        //    obpost.PostType = "Image";
        //    obpost.PostDateTime = DateTime.Now;
        //    obpost.ModifiedOn = DateTime.Now;
        //    obpost.CreatedOn = DateTime.Now;

        //    db.NGOPosts.Add(obpost);
        //    db.SaveChanges();


        //    return RedirectToAction("Index", "Register");

        //}
        public ActionResult Setting()
        {
            return View();
        }
    }
}
