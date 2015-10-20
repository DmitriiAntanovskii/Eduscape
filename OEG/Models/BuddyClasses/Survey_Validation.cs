using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace OEG.Models
{
    [MetadataType(typeof(SurveysMetadata))]
    public partial class Surveys {
        public string CreatedByName
        {
            get
            {
                oeg_reportsEntities db = new oeg_reportsEntities();

                User u = db.Users.Find(this.CreatedBy);

                return u.FirstName + " " + u.Surname;
            }
        }

        public string ModifedByName
        {
            get
            {
                oeg_reportsEntities db = new oeg_reportsEntities();

                User u = db.Users.Find(this.ModifedBy);

                return u.FirstName + " " + u.Surname;
            }
        }
    }

    //And a metadata class    
    public class SurveysMetadata
    {
        [Required]
        [Display(Name = "Survey Name")]
        public string SurveyName { get; set; }
        [Required]
        [Display(Name = "Survey Code")]
        public string SurveyCode { get; set; }
    }
}