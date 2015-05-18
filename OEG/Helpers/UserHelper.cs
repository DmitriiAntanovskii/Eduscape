using OEG.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace OEG.Static_Helper
{
    public class UserHelper
    {
        public static User getMember(oeg_reportsEntities db)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            string email = identity.Identities.First().Name;
            User u = (from a in db.Users
                             where a.Email == email
                             select a).FirstOrDefault();
            return u;
           
        }
    }
}