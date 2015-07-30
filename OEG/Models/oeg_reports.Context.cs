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
    
        public virtual ObjectResult<StdDevByFactor_Result> StdDevByFactor()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StdDevByFactor_Result>("StdDevByFactor");
        }
    
        public virtual ObjectResult<StdDevByQuestionID_Result> StdDevByQuestionID()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StdDevByQuestionID_Result>("StdDevByQuestionID");
        }
    
        public virtual ObjectResult<YearLevelBenchmark_Result> YearLevelBenchmark(string jobCodes)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<YearLevelBenchmark_Result>("YearLevelBenchmark", jobCodesParameter);
        }
    
        public virtual ObjectResult<ProgramsBenchmark_Result> ProgramsBenchmark(string factors)
        {
            var factorsParameter = factors != null ?
                new ObjectParameter("Factors", factors) :
                new ObjectParameter("Factors", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProgramsBenchmark_Result>("ProgramsBenchmark", factorsParameter);
        }
    
        public virtual ObjectResult<DurationBenchmark_Result> DurationBenchmark(string days)
        {
            var daysParameter = days != null ?
                new ObjectParameter("Days", days) :
                new ObjectParameter("Days", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DurationBenchmark_Result>("DurationBenchmark", daysParameter);
        }
    
        public virtual ObjectResult<AllCoursesBenchmark_Result> AllCoursesBenchmark(string jobCodes)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AllCoursesBenchmark_Result>("AllCoursesBenchmark", jobCodesParameter);
        }
    
        public virtual ObjectResult<QuantativeByGroup_Result> QuantativeByGroup(string jobCodes, string empNo)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<QuantativeByGroup_Result>("QuantativeByGroup", jobCodesParameter, empNoParameter);
        }
    
        public virtual ObjectResult<SchoolQuantativeByGroup_Result> SchoolQuantativeByGroup(string jobCodes, string empNo)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SchoolQuantativeByGroup_Result>("SchoolQuantativeByGroup", jobCodesParameter, empNoParameter);
        }
    
        public virtual ObjectResult<SchoolQualative_Result> SchoolQualative(string years, string schools, string jobCodes, string venues, string startDates, string empNo)
        {
            var yearsParameter = years != null ?
                new ObjectParameter("Years", years) :
                new ObjectParameter("Years", typeof(string));
    
            var schoolsParameter = schools != null ?
                new ObjectParameter("Schools", schools) :
                new ObjectParameter("Schools", typeof(string));
    
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var venuesParameter = venues != null ?
                new ObjectParameter("Venues", venues) :
                new ObjectParameter("Venues", typeof(string));
    
            var startDatesParameter = startDates != null ?
                new ObjectParameter("StartDates", startDates) :
                new ObjectParameter("StartDates", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SchoolQualative_Result>("SchoolQualative", yearsParameter, schoolsParameter, jobCodesParameter, venuesParameter, startDatesParameter, empNoParameter);
        }
    }
}
