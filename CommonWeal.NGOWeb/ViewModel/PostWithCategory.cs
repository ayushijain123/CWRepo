using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel
{
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