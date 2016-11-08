using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;
using System.Net.Http;
using CommonWeal.NGOWeb.ViewModel;

namespace CommonWeal.NGOWeb.Controllers.NGO
{
   [Authorize]
    public class PostController : BaseController
    {

        [HttpPost]

        public void SumitComment(string strComment, int postId)
       {
           if (strComment != null)
           {
               CommonWealEntities db = new CommonWealEntities();
               PostComment postcmnt = new PostComment();
               postcmnt.CommentText = strComment;
               postcmnt.CreatedOn = DateTime.Now;
               postcmnt.ModifiedOn = DateTime.Now;
               postcmnt.PostID = postId;
               postcmnt.UserID = User.Identity.Name;

               db.PostComments.Add(postcmnt);
               db.SaveChanges();

           }

        }


        [HttpPost]

        public void SubmitLike(bool like, int PostID=-1)
        {
            if (PostID !=-1)
            {
                CommonWealEntities db = new CommonWealEntities();

                string EmailID = User.Identity.Name;
                int UserID = db.Users.Where(usr => usr.LoginEmailID == EmailID).FirstOrDefault().LoginID;
                var currentLikeUser = db.PostLikes.Where(pstlike => pstlike.PostID == PostID & pstlike.UserID == UserID).FirstOrDefault();
                if (currentLikeUser == null)
                {
                    PostLike pl = new PostLike();
                    pl.CreatedOn = DateTime.Now;
                    pl.ModifiedOn = DateTime.Now;
                    pl.IsLike = true;

                    pl.UserID = UserID;
                    pl.PostID = PostID;
                    var post = db.NGOPosts.Where(ngpost => ngpost.PostID == PostID).FirstOrDefault();
                    post.PostLikeCount++;
                    db.PostLikes.Add(pl);

                    db.SaveChanges();
                }
            }


        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult getLikeList(int postid)
        {
            CommonWealEntities db = new CommonWealEntities();
            List<PostLikeModel> postlikelist = new List<PostLikeModel>();
          
             var likeuserlist= db.PostLikes.Where(pstlike => pstlike.PostID == postid).ToList();
            foreach(var item in likeuserlist)
            {
                PostLikeModel plm = new PostLikeModel();
              int userType = db.Users.Where(user => user.LoginID == item.UserID).FirstOrDefault().LoginUserType;

                    switch (userType)
                    {
                        case 1: string NGOUser = db.NGOUsers.Where(ngusr => ngusr.LoginID == item.UserID).FirstOrDefault().NGOName.ToString();
                            plm.userName= NGOUser;
                            break;
                        case 3: var RegUser =db.RegisteredUsers.Where(lgnuser => lgnuser.LoginID == item.UserID).FirstOrDefault();
                           plm.userName = RegUser.FirstName + " " + RegUser.LastName;
                            break;

                    }
                plm.userImageUrl = "";
                plm.UserID = item.UserID;
                postlikelist.Add(plm);

            }
            
           
           return Json(postlikelist,JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //[AllowAnonymous]
        //public string GetPosts([From] FormCollection myobj)
        //{
        //    CommonWealEntities db = new CommonWealEntities();



        //    return "sdfasdfasdfasd";
        //}


        //public class mycls
        //{

        //    public int i { get; set; }
        //    public int j { get; set; }
        //    public int k { get; set; }

        //}





    }
}
