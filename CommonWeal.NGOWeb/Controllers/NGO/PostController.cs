using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonWeal.Data;

namespace CommonWeal.NGOWeb.Controllers.NGO
{
    [Authorize]
    public class PostController : BaseController
    {

        [HttpPost]

        public void SumitComment(string strComment, int postId)
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


        [HttpPost]

        public void SubmitLike(bool like,int PostID)
        {
            CommonWealEntities db = new CommonWealEntities();
            PostLike pl = new PostLike();
            pl.CreatedOn = DateTime.Now;
            pl.ModifiedOn = DateTime.Now;
            pl.IsLike = true;
            string EmailID = User.Identity.Name;
           int UserID= db.Users.Where(usr => usr.LoginEmailID == EmailID).FirstOrDefault().LoginID;
            pl.UserID = UserID;
            pl.PostID = PostID;
           var currentLikeUser= db.PostLikes.Where(pstlike => pstlike.PostID == PostID & pstlike.UserID == UserID);
           if (currentLikeUser == null)
           {        
               var post=db.NGOPosts.Where(ngpost => ngpost.PostID == PostID).FirstOrDefault();
               post.PostLikeCount++;
               db.PostLikes.Add(pl);

               db.SaveChanges();
           }
           
            



        }


    }
}
