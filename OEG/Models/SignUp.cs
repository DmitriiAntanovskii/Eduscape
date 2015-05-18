using System;
using System.ComponentModel.DataAnnotations;

/*
multiple sign up classes required for A/B testing purposes
37 signals foudn that by requiring a first/user name as well as email they got a 34% better
conversion rate?!?! crazy!! so we should test all of the things. 
*/

#region UsableClasses : Classes for use now
//all sign ups must have email
public class MinSignUp
{
    [Required]
    [StringLength(255)]
    [EmailAddress]
    [Display(Name = "Email")]
    public string email { get; set; }
}

public class SourceMinSignUp : MinSignUp
{
    [Required]
    [StringLength(50)]
    public string source { get; set; }

}

//firstname and email
public class FirstNameSignUp : MinSignUp
{
    [Required]
    [StringLength(50)]
    public string firstname { get; set; }
}

//firstname, surname and email
public class FullNameSignUp : FirstNameSignUp
{
    [Required]
    [StringLength(50)]
    public string surname { get; set; }
}

#endregion

#region FutureClasses : Classes writting for potential use in the future
//these classes wirtten for potential future use though by this stage
//we'd be keeping the data in our own db and not in mailchimp only

//username and email
public class UserNameSignUp : MinSignUp
{
    public string username { get; set; }
}

//password and email
public class PasswordSignUp : MinSignUp
{
    
    [Required]
    [StringLength(50, ErrorMessage = "The password must be at least 8 characters long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string password { get; set; }
    [Display(Name = "Remember me")]
    public bool rememberme { get; set; }
}

//username, passowrd and email
public class UserNamePassowrdSignUp : UserNameSignUp
{
    public string password { get; set; }
}

//firstname, surname, username, password and email
public class FullSignUp : UserNameSignUp
{
    public string username { get; set; }
    public string password { get; set; }
}

//shits and giggles yo
public class FullMegaEpicSignUp : FullSignUp
{
    public string phone { get; set; }
    public string address { get; set; }
    public string suburb { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string favcolour { get; set; }
}

#endregion
