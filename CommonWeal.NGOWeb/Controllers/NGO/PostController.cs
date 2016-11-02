using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeal.NGOWeb.Controllers.NGO
{
    [Authorize]
    public class PostController : BaseController
    {

        [HttpPost]

        public void SumitComment(string strComment, int postId)
        {
            CommonWealEntities1 db = new CommonWealEntities1();
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
