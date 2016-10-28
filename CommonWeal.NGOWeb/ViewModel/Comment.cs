using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeal.NGOWeb.ViewModel
{

    public class Comment
    {
       
        public String Username { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public String commentContent { get; set; }
        public int commentLike { get; set; }
        public String commentUserImage { get; set; }
        public String commentId { get; set; }

    }
}