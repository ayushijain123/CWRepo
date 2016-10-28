using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel
{
    public class Post
    {
       
        public int postId { get; set; }
        public String userName { get; set; }
        public String userImage { get; set; }
        public DateTime postCreateTime { get; set; }
        public String postImageUrl{ get; set; }
        public int likeCount { get; set; }
        public int commentCount { get; set; }
        public String postcontent { get; set; }

        public List<Comment> PostComments { get; set; }
    } 
}