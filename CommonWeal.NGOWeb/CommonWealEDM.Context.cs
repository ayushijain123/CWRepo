﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CommonWealEntities1 : DbContext
    {
        public CommonWealEntities1()
            : base("name=CommonWealEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<NGOPost> NGOPosts { get; set; }
        public DbSet<NGOPostDetail> NGOPostDetails { get; set; }
        public DbSet<NGOUser> NGOUsers { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
    }
}
