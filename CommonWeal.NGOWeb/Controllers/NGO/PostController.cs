using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;
using System.Net.Http;
using CommonWeal.NGOWeb.ViewModel;
using System.Data.Entity;
namespace CommonWeal.NGOWeb.Controllers.NGO
{
    [Authorize]
    public class PostController : BaseController
    {
        /*action for commenting on a post through post*/
        [HttpPost]
        public JsonResult SumitComment(string strComment, int postId)
        {
            string userName = "";
            List<string> userinfo = new List<string>();
            if (strComment != null)
            {
                CommonWealEntities db = new CommonWealEntities();
                PostComment postcmnt = new PostComment();
                postcmnt.CommentText = strComment;
                postcmnt.CreatedOn = DateTime.Now;
                postcmnt.ModifiedOn = DateTime.Now;
                postcmnt.PostID = postId;
                /*login user property defined in base controller*/
                postcmnt.LoginID = LoginUser.LoginID;
                //Convert.ToInt32(User.Identity.Name);

                db.PostComments.Add(postcmnt);
                db.SaveChanges();
                /*update like count in post table  */
                var post = db.NGOPosts.Where(ngpost => ngpost.PostID == postId).FirstOrDefault();
                post.PostLikeCount = db.PostComments.Where(x => x.PostID == postId).Count();
                db.SaveChanges();
                /*getting user type for getting name from user or ngo table dynamically by switch case */
                int userType = db.Users.Where(user => user.LoginID == LoginUser.LoginID).FirstOrDefault().LoginUserType;

                switch (userType)
                {
                    case 1: string NGOUser = db.NGOUsers.Where(ngusr => ngusr.LoginID == LoginUser.LoginID).FirstOrDefault().NGOName.ToString();
                        userName = NGOUser;
                        break;
                    case 3: var RegUser = db.RegisteredUsers.Where(lgnuser => lgnuser.LoginID == LoginUser.LoginID).FirstOrDefault();
                        userName = RegUser.FirstName + " " + RegUser.LastName;
                        break;

                }

                userinfo.Add(userName);
                userinfo.Add(postcmnt.CreatedOn.ToString());
            }
            return Json(userinfo, JsonRequestBehavior.AllowGet);
        }



        /*action for submit like through ajax*/
        [HttpPost]

        public void SubmitLike(bool like, int PostID = -1)
        {
            if (PostID != -1)
            {
                CommonWealEntities db = new CommonWealEntities();

                /*login user property defined in base controller*/
                /*checking is current login user already liked the image or not */
                var currentLikeUser = db.PostLikes.Where(pstlike => pstlike.PostID == PostID & pstlike.LoginID == LoginUser.LoginID).FirstOrDefault();

                if (currentLikeUser == null)
                {
                    /*if not like than add row in post  */
                    PostLike pl = new PostLike();
                    pl.CreatedOn = DateTime.Now;
                    pl.ModifiedOn = DateTime.Now;
                    pl.IsLike = true;
                    /*login user property defined in base controller*/
                    pl.LoginID = LoginUser.LoginID;
                    pl.PostID = PostID;
                    db.PostLikes.Add(pl);
                    db.SaveChanges();
                    /*update like count */
                    var post = db.NGOPosts.Where(ngpost => ngpost.PostID == PostID).FirstOrDefault();
                  //  post.PostLikeCount = db.PostLikes.Where(x => x.PostID == PostID).Count();
                    post.PostLikeCount++;
                    db.SaveChanges();
                }
                else
                {/*if already liked by user than remove like row of user for unlike */
                    var removeLike = db.PostLikes.Where(pstlike => pstlike.PostID == PostID & pstlike.LoginID == LoginUser.LoginID).FirstOrDefault();
                    db.PostLikes.Remove(removeLike);
                    var post = db.NGOPosts.Where(ngpost => ngpost.PostID == PostID).FirstOrDefault();
                    post.PostLikeCount--;
                    db.SaveChanges();               
                 }
            }


        }

        /*action for getting like list of particular post*/
        [AllowAnonymous]
        [HttpPost]
        public JsonResult getLikeList(int postid)
        {
            CommonWealEntities db = new CommonWealEntities();

            List<PostLikeModel> postlikelist = new List<PostLikeModel>();

            var likeuserlist = db.PostLikes.Where(pstlike => pstlike.PostID == postid).ToList();
            foreach (var item in likeuserlist)
            {
                /*getting post like list and filling in post like model list */
                PostLikeModel plm = new PostLikeModel();
                int userType = db.Users.Where(user => user.LoginID == item.LoginID).FirstOrDefault().LoginUserType;

                switch (userType)
                {
                    case 1: string NGOUser = db.NGOUsers.Where(ngusr => ngusr.LoginID == item.LoginID).FirstOrDefault().NGOName.ToString();
                        plm.userName = NGOUser;
                        break;
                    case 3: var RegUser = db.RegisteredUsers.Where(lgnuser => lgnuser.LoginID == item.LoginID).FirstOrDefault();
                        plm.userName = RegUser.FirstName + " " + RegUser.LastName;
                        break;

                }
                plm.userImageUrl = "";
                plm.UserID = item.LoginID;
                postlikelist.Add(plm);

            }


            return Json(postlikelist, JsonRequestBehavior.AllowGet);
        }



        /*method for getting next slot of posts on click of load more button*/
        //[HttpPost]
        [AllowAnonymous]
        public PartialViewResult onLoadPost(int count=0,int category=0 ,int NgoID=0)
        {
            try
            {
                var load = new List<Post>();
                CommonWealEntities db = new CommonWealEntities();
                dbOperations ob = new dbOperations();
                if (NgoID == 1)
                {
                    NgoID = LoginUser.LoginID;
                    load = ob.GetPostOnSeeMore(count, category, NgoID);
                }
                else {
                    load = ob.GetPostOnSeeMore(count, category, NgoID);
                }

                /*returing list to  partial view and than partial view is retuned to ajax call  */
                return PartialView("~/views/userHome/_Posts.cshtml", load);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /*action for getting  post category wise*/
        [AllowAnonymous]
        public PartialViewResult GetPostByCategory2(int[] category)
        { 
        var load =new List<Post>();
        load = null;
        if (category!=null && category.Count() >= 0)
        {
            dbOperations ob = new dbOperations();
            CommonWealEntities db = new CommonWealEntities();
            //var list = db.WorkingAreas.Where(x => x.CategoryID == category).ToList();
            // var post = db.NGOPosts.Include

            //var query = objEntities.Employee.Join(objEntities.Department, r => r.EmpId, p => p.EmpId, (r,p) => new{r.FirstName, r.LastName, p.DepartmentName});
            //var query = db.NGOPosts.Join(db.PostCategories, ngoPost => ngoPost.PostID, postCategories => postCategories.PostID, (r, p) => new { r }).ToList();
            // var query1 = db.NGOPosts.Include(x => x.PostCategories).Where(w => w.PostCategories.Where(m => m.CategoryID == category).Any()).ToList();
            load = ob.GetPostByCategory1(category);


        }
      
            return PartialView("~/views/userHome/_Posts.cshtml", load);
        }

        [AllowAnonymous]
        public JsonResult getpostCount()
        {
            var result1=BaseController.pageleft;
            return Json(result1, JsonRequestBehavior.AllowGet);
        }
    }
}
