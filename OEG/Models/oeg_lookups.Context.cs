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
    
    public partial class oeg_lookupsEntities : DbContext
    {
        public oeg_lookupsEntities()
            : base("name=oeg_lookupsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<GetEmployees_Result> GetEmployees(string employeeNumbers)
        {
            var employeeNumbersParameter = employeeNumbers != null ?
                new ObjectParameter("EmployeeNumbers", employeeNumbers) :
                new ObjectParameter("EmployeeNumbers", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmployees_Result>("GetEmployees", employeeNumbersParameter);
        }
    
        public virtual ObjectResult<GetPrograms_Result> GetPrograms(string jobCodes)
        {
            var jobCodesParameter = jobCodes != null ?
                new ObjectParameter("JobCodes", jobCodes) :
                new ObjectParameter("JobCodes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPrograms_Result>("GetPrograms", jobCodesParameter);
        }
    
        public virtual ObjectResult<getEmployeeIDByName_Result> getEmployeeIDByName(string firstname, string lastname)
        {
            var firstnameParameter = firstname != null ?
                new ObjectParameter("firstname", firstname) :
                new ObjectParameter("firstname", typeof(string));
    
            var lastnameParameter = lastname != null ?
                new ObjectParameter("lastname", lastname) :
                new ObjectParameter("lastname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getEmployeeIDByName_Result>("getEmployeeIDByName", firstnameParameter, lastnameParameter);
        }
    
        public virtual ObjectResult<GetRosteredJobcodesCSVByEmployeeNumbers_Result> GetRosteredJobcodesCSVByEmployeeNumbers(string inputEmpID_Str, Nullable<int> monthsAhead, Nullable<int> monthsPrevious)
        {
            var inputEmpID_StrParameter = inputEmpID_Str != null ?
                new ObjectParameter("inputEmpID_Str", inputEmpID_Str) :
                new ObjectParameter("inputEmpID_Str", typeof(string));
    
            var monthsAheadParameter = monthsAhead.HasValue ?
                new ObjectParameter("monthsAhead", monthsAhead) :
                new ObjectParameter("monthsAhead", typeof(int));
    
            var monthsPreviousParameter = monthsPrevious.HasValue ?
                new ObjectParameter("monthsPrevious", monthsPrevious) :
                new ObjectParameter("monthsPrevious", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRosteredJobcodesCSVByEmployeeNumbers_Result>("GetRosteredJobcodesCSVByEmployeeNumbers", inputEmpID_StrParameter, monthsAheadParameter, monthsPreviousParameter);
        }
    }
}
