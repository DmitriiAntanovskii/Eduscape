using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OEG.Models.CustomValidation
{
    public class UniqueEmail : ValidationAttribute
    {
        private oeg_reportsEntities db = new oeg_reportsEntities();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var owner = validationContext.ObjectInstance as User;
            if (owner == null) return new ValidationResult("Model is empty");
            ;
            if (value != null) //COB is non mandatory so null is acceptable
            {
                User e = db.Users.Where(x => x.Email == value && x.UserID != owner.UserID).FirstOrDefault();
                if (e != null)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage, new[] { validationContext.MemberName });
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}