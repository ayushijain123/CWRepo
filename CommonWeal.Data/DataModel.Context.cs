﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommonWeal.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class CommonWealEntities : DbContext
    {
        public CommonWealEntities()
            : base("name=CommonWealEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<NGOPost> NGOPosts { get; set; }
        public DbSet<NGOPostDetail> NGOPostDetails { get; set; }
        public DbSet<NGOUser> NGOUsers { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
    
        
    }
}
