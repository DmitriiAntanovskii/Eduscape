//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class User
    {
        public int UserID { get; set; }
        public System.Guid UserGUID { get; set; }
        public int UserGroupID { get; set; }
        public string Email { get; set; }
        public string PWD { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual UserGroup UserGroup { get; set; }
    }
}
