using OEG.Models.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace OEG.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User {
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
    public class UserMetadata
    {
        [Required]
        [Display(Name = "User Group Name")]
        public int UserGroupID { get; set; }
        [Required]
        [UniqueEmail(ErrorMessage="This email address already in use.")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}