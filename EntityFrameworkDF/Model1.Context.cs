﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFrameworkDF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbSınavEntities : DbContext
    {
        public DbSınavEntities()
            : base("name=DbSınavEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<dersler> dersler { get; set; }
        public virtual DbSet<kulupler> kulupler { get; set; }
        public virtual DbSet<notlar> notlar { get; set; }
        public virtual DbSet<ogrenci> ogrenci { get; set; }
    
        public virtual ObjectResult<Notlistesi_Result> Notlistesi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Notlistesi_Result>("Notlistesi");
        }
    }
}