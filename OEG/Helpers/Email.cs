using OEG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace OEG.Helpers
{
    public class Email
    {
        public static void SendEmail(string to, string from, string subject, string message)
        {
            MailMessage mail = new MailMessage(from, to);
            SmtpClient client = new SmtpClient();
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            client.Send(mail);
        }

        private static string createEmail(string body)
        {
            string msg;
            //email header
            msg = "<html><head><style>";
            msg += "body {text-align: center; font-family: Arial;} #container {margin: 0 auto;width: 900px; text-align: left;} #container img {width:100%;}</style></head><body>";
            msg += @"<div id=""container""><a href=""http://oeg.thinksimple.com.au""><img src=""https://oeg.thinksimple.com.au/img/oeglogo.png"" alt=""OEG"" /></a>";
            //content
            msg += body;
            //footer
            msg += @"Kind Regards<br><a href=""http://oeg.thinksimple.com.au"">OEG</a>";
            msg += "</div></body></html>";

            return msg;
        }

        //public static string ActivationEmail(Member m)
        //{
        //    string msg;
        //    string link = "https://OEGReports.com.au/member/activate?id=" + m.MemberGUID;

        //    msg = "<h1>Welcome to OEG</h1>";
        //    msg += "<p>To activate your account please click the link below:</p>";
        //    msg += @"<a target=""_blank"" href=""" + link + @""">" + link + "</a><br>"; 
        //    msg += "<p>If the above link does not work simply copy it into the address bar of your browser.</p>";

        //    return createEmail(msg);
        //}

        //public static string ConfirmEmail(Member m)
        //{
        //    string msg;
        //    string link = "https://OEGReports.com.au/member/confirm?id=" + m.MemberGUID;

        //    msg = "<h1>Welcome to OEG</h1>";
        //    msg += "<p>To activate your account please click the link below:</p>";
        //    msg += @"<a target=""_blank"" href=""" + link + @""">" + link + "</a><br>";
        //    msg += "<p>If the above link does not work simply copy it into the address bar of your browser.</p>";

        //    return createEmail(msg);
        //}


        public static string ForgotPasswordEmail(string PWD)
        {
            string msg;

            msg = "<h1>OEG - Password Reset</h1>";
            msg += "<p>We have reset your password: <b>" + PWD + "</b></p>";
            msg += "<p>If you did not request this password reset please let us know.</p>";
            //msg += @"<p>To log in click <a href=""https://OEGReports.com.au/Member/Login"">HERE</a></p>";

            return createEmail(msg);
        }

        public static string NewUserEmail(string PWD)
        {
            string msg;

            msg = "<h1>OEG - Reports</h1>";
            msg += "<p>A user account has been created for you. Your Password is: <b>" + PWD + "</b></p>";
            msg += "<p>If you did not request this password reset please let us know.</p>";
            //msg += @"<p>To log in click <a href=""https://OEGReports.com.au/Member/Login"">HERE</a></p>";

            return createEmail(msg);
        }


    
    }
}