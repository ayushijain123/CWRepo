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
    
    public partial class RegisteredUser
    {
        
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserKey { get; set; }
        public string Mobile { get; set; }
        public int LoginID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> LoginUserType { get; set; }
    
        public virtual UserLogin UserLogin { get; set; }
    }
}