using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel
{
    public class Post
    {
        public int userId { get; set; }
        public int postId { get; set; }
        public string userName { get; set; }
        public string userImage { get; set; }
        public DateTime postCreateTime { get; set; }
        public string postImageUrl{ get; set; }
        public int likeCount { get; set; }
        public int commentCount { get; set; }
        public string postcontent { get; set; }
        public List<PostLikeModel> postlike { get; set; }
        public List<Comment> PostComments { get; set; }
        public string categoryName { get; set; }
        public List<string> postCategoryNameList { get; set; }
    } 
}