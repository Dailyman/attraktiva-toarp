﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventHandlingSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ATEntities : DbContext
    {
        public ATEntities()
            : base("name=ATEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<association_permissions> association_permissions { get; set; }
        public DbSet<associations> associations { get; set; }
        public DbSet<categories> categories { get; set; }
        public DbSet<communities> communities { get; set; }
        public DbSet<community_permissions> community_permissions { get; set; }
        public DbSet<components> components { get; set; }
        public DbSet<controls> controls { get; set; }
        public DbSet<events> events { get; set; }
        public DbSet<filterdata> filterdata { get; set; }
        public DbSet<members> members { get; set; }
        public DbSet<subcategories> subcategories { get; set; }
        public DbSet<users> users { get; set; }
        public DbSet<webpages> webpages { get; set; }
    }
}
