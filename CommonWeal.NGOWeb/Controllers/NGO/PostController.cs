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

    }
}
