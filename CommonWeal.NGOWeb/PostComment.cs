//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommonWeal.NGOWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class PostComment
    {
        public int CommentID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> PostID { get; set; }
        public string CommentText { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}