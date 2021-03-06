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
        public virtual DbSet<Surveys> Surveys { get; set; }
        public virtual DbSet<StaffReportData> StaffReportData { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<tblHR_Entities> tblHR_Entities { get; set; }
        public virtual DbSet<tblProgram> tblPrograms { get; set; }
    
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
    
        public virtual ObjectResult<Duration_Result> Duration(string days)
        {
            var daysParameter = days != null ?
                new ObjectParameter("Days", days) :
                new ObjectParameter("Days", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Duration_Result>("Duration", daysParameter);
        }
    
        public virtual ObjectResult<Venue_Result> Venue()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Venue_Result>("Venue");
        }
    
        public virtual ObjectResult<School_Result> School()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<School_Result>("School");
        }
    
        public virtual ObjectResult<JobCodePrograms_Result> JobCodePrograms(string factors)
        {
            var factorsParameter = factors != null ?
                new ObjectParameter("Factors", factors) :
                new ObjectParameter("Factors", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<JobCodePrograms_Result>("JobCodePrograms", factorsParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> ItemJobCodeSubTotal(string jobCodes, string groups, string empNo)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var groupsParameter = groups != null ?
                new ObjectParameter("Groups", groups) :
                new ObjectParameter("Groups", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("ItemJobCodeSubTotal", jobCodesParameter, groupsParameter, empNoParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> JobCodeGroupSubTotal(string jobCodes, string groups, string empNo)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var groupsParameter = groups != null ?
                new ObjectParameter("Groups", groups) :
                new ObjectParameter("Groups", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("JobCodeGroupSubTotal", jobCodesParameter, groupsParameter, empNoParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> SchoolQuantativeByGroupSubTotal(string jobCodes, string groups, string empNo)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var groupsParameter = groups != null ?
                new ObjectParameter("Groups", groups) :
                new ObjectParameter("Groups", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("SchoolQuantativeByGroupSubTotal", jobCodesParameter, groupsParameter, empNoParameter);
        }
    
        public virtual ObjectResult<YearLevel_Result> YearLevel(string jobCodes)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<YearLevel_Result>("YearLevel", jobCodesParameter);
        }
    
        public virtual ObjectResult<SchoolQualative_Result> SchoolQualative(string years, string schools, string jobCodes, string groups, string venues, string startDates, string empNo)
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
    
            var groupsParameter = groups != null ?
                new ObjectParameter("Groups", groups) :
                new ObjectParameter("Groups", typeof(string));
    
            var venuesParameter = venues != null ?
                new ObjectParameter("Venues", venues) :
                new ObjectParameter("Venues", typeof(string));
    
            var startDatesParameter = startDates != null ?
                new ObjectParameter("StartDates", startDates) :
                new ObjectParameter("StartDates", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SchoolQualative_Result>("SchoolQualative", yearsParameter, schoolsParameter, jobCodesParameter, groupsParameter, venuesParameter, startDatesParameter, empNoParameter);
        }
    
        public virtual ObjectResult<Item_Result> Item()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Item_Result>("Item");
        }
    
        public virtual ObjectResult<ItemJobCode_Result> ItemJobCode(string jobCodes, string groups, string empNo)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var groupsParameter = groups != null ?
                new ObjectParameter("Groups", groups) :
                new ObjectParameter("Groups", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ItemJobCode_Result>("ItemJobCode", jobCodesParameter, groupsParameter, empNoParameter);
        }
    
        public virtual ObjectResult<JobCodeGroup_Result> JobCodeGroup(string jobCodes, string groups, string empNo)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var groupsParameter = groups != null ?
                new ObjectParameter("Groups", groups) :
                new ObjectParameter("Groups", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<JobCodeGroup_Result>("JobCodeGroup", jobCodesParameter, groupsParameter, empNoParameter);
        }
    
        public virtual ObjectResult<SchoolQuantativeByGroup_Result> SchoolQuantativeByGroup(string jobCodes, string groups, string empNo)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            var groupsParameter = groups != null ?
                new ObjectParameter("Groups", groups) :
                new ObjectParameter("Groups", typeof(string));
    
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SchoolQuantativeByGroup_Result>("SchoolQuantativeByGroup", jobCodesParameter, groupsParameter, empNoParameter);
        }
    
        public virtual ObjectResult<GroupsByEmployee_Result> GroupsByEmployee(string empNo)
        {
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GroupsByEmployee_Result>("GroupsByEmployee", empNoParameter);
        }
    
        public virtual ObjectResult<Competency_Result> Competency(string jobCodes)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Competency_Result>("Competency", jobCodesParameter);
        }
    
        public virtual ObjectResult<YearLevelBenchmark_Result> YearLevelBenchmark(string jobCodes)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<YearLevelBenchmark_Result>("YearLevelBenchmark", jobCodesParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> GroupsByEmployeeSubTotal(string empNo)
        {
            var empNoParameter = empNo != null ?
                new ObjectParameter("EmpNo", empNo) :
                new ObjectParameter("EmpNo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("GroupsByEmployeeSubTotal", empNoParameter);
        }
    }
}
