﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OEG.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class oeg_reportsEntities : DbContext
    {
        public oeg_reportsEntities()
            : base("name=oeg_reportsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ReportData> ReportDatas { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<SchoolReport_Result> SchoolReport()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SchoolReport_Result>("SchoolReport");
        }
    
        public virtual ObjectResult<DurationBenchmark_Result> DurationBenchmark()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DurationBenchmark_Result>("DurationBenchmark");
        }
    
        public virtual ObjectResult<ProgramsBenchmark_Result> ProgramsBenchmark()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProgramsBenchmark_Result>("ProgramsBenchmark");
        }
    }
}
