﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Artists.Subscribers.DataAccess.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SubscribersDBEntities : DbContext
    {
        public SubscribersDBEntities()
            : base("name=SubscribersDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TopArtistsSubscribers> TopArtistsSubscribers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
