﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PVA.Web.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PhysicalVerification_HUL_NewEntities : DbContext
    {
        public PhysicalVerification_HUL_NewEntities()
            : base("name=PhysicalVerification_HUL_NewEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FARToBeVerified> FARToBeVerifieds { get; set; }
        public virtual DbSet<PlantAreaMaster> PlantAreaMasters { get; set; }
    }
}
