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
}