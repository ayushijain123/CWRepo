//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace NGOUserPage
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_login
    {
        public tbl_login()
        {
            this.tbl_comment = new HashSet<tbl_comment>();
            this.tbl_commentlike = new HashSet<tbl_commentlike>();
            this.tbl_post = new HashSet<tbl_post>();
            this.tbl_postlike = new HashSet<tbl_postlike>();
            this.tbl_user = new HashSet<tbl_user>();
        }
    
        public long login_id { get; set; }
        [DataType(DataType.Password)]
        public string login_password { get; set; }
        public string login_userType { get; set; }

        public string login_Email { get; set; }
        public Nullable<int> isAccepted { get; set; }
    
        public virtual ICollection<tbl_comment> tbl_comment { get; set; }
        public virtual ICollection<tbl_commentlike> tbl_commentlike { get; set; }
        public virtual ICollection<tbl_post> tbl_post { get; set; }
        public virtual ICollection<tbl_postlike> tbl_postlike { get; set; }
        public virtual ICollection<tbl_user> tbl_user { get; set; }
    }
}
