using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OEG.Models
{
    public class StdDevViewModel
    {
        public List<StdDevByFactor_Result> Factor { get; set; }
        public List<StdDevByQuestionID_Result> Question { get; set; }
    }

    public class JobCodeGroupViewModel
    {
        public List<JobCodeGroup_Result> ReportData { get; set; }
        public double? SubTotal { get; set; }
    }

    public class SchoolQuantitativeByGroupViewModel
    {
        public List<SchoolQuantativeByGroup_Result> ReportData { get; set; }
        public double? SubTotal { get; set; }
    }

    public class ItemJobCodeViewModel
    {
        public List<ItemJobCode_Result> ReportData { get; set; }
        public double? SubTotal { get; set; }
    }

    public class GroupsByEmployeeViewModel
    {
        public List<GroupsByEmployee_Result> ReportData { get; set; }
        public double? SubTotal { get; set; }
    }


}