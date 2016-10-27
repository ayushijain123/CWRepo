using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.Models
{
    public class PostModel
    {
        public List<CommentModel> comment { get; set; }
        public int postId { get; set; }
        public String userName { get; set; }
        public String userImage { get; set; }
        public DateTime postCreateTime { get; set; }
        public String postImageUrl{ get; set; }
        public int likeCount { get; set; }
        public String commentCount { get; set; }
        public String commentlist { get; set; }


    }
    public class CommentModel {
        public String commentUsername { get; set; }
        public DateTime commentDateTime { get; set; }
        public String commentContent { get; set; }
        public int commentLike { get; set; }
        public String commentUserImage { get; set; }
        public String commentId { get; set; }

    }
}