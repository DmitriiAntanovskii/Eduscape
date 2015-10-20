using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace OEG.Models
{
    [MetadataType(typeof(UserGroupMetadata))]
    public partial class UserGroup {}

    //And a metadata class    
    public class UserGroupMetadata
    {
        [Required]
        [Display(Name = "User Group Name")]
        public string UserGroupName { get; set; }

    }
}