using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeal.Data.ModelExtension
{
    public class PostWithTopNgo
    {
        public List<Post> post { get; set; }
        public List<NGOUser> ngouser { get; set; }


    }


    public class Post
        {
            public int userId { get; set; }
            public int postId { get; set; }
            public string userName { get; set; }
            public string userImage { get; set; }
            public DateTime postCreateTime { get; set; }
            public string postImageUrl { get; set; }
            public int likeCount { get; set; }
            public int commentCount { get; set; }
            public string postcontent { get; set; }
            public List<PostLikeModel> postlike { get; set; }
            public List<Comment> PostComments { get; set; }
            public string categoryName { get; set; }
            public List<string> postCategoryNameList { get; set; }
            public string controllername { get; set; }
            public bool IsRequest { get; set; }
            public List<DonateItem> donateItemlist { get; set; }
    
    }

    public class PostLikeModel
    {
        public int postId { get; set; }
        public string userName { get; set; }
        public string userImageUrl { get; set; }
        public int UserID { get; set; }
        public bool IsDelete { get; set; }

    }
    public class Comment
    {

        public string Username { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string commentContent { get; set; }
        public int commentLike { get; set; }
        public string commentUserImage { get; set; }
        public int commentId { get; set; }
        public int commentUserId { get; set; }
        public bool IsDelete { get; set; }

    }
    public class PostWithCategory
    {
        public int PostCategoryId { get; set; }
        public int CategoryID { get; set; }
        public int PostID { get; set; }
        public int? LoginID { get; set; }
        public string PostType { get; set; }
        public string PostContent { get; set; }
        public string PostUrl { get; set; }
        public int PostLikeCount { get; set; }
        public Nullable<int> PostCommentCount { get; set; }
        public DateTime PostDateTime { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public List<string> CategoryList { get; set; }
        public List<int> CategoryIdList { get; set; }
        public int RequestID { get; set; }
        public bool IsRequest { get; set; }
    }
}
