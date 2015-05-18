using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace OEG.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User { }

    //And a metadata class    
    public class UserMetadata
    {
        [Required]
        [Display(Name = "User Group Name")]
        public int UserGroupID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }

    }
}