using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel
{

    public class Comment
    {
       
        public string Username { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string commentContent { get; set; }
        public int commentLike { get; set; }
        public string commentUserImage { get; set; }
        public int commentId { get; set; }
        public int commentUserId { get; set; }

    }
}