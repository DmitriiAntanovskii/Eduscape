﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OEG.Models;
using OEG.Helpers;
using System.Web.Configuration;
using System.Web.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Data.Entity.Validation;
using OEG.Static_Helper;


namespace OEG.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private oeg_reportsEntities db = new oeg_reportsEntities();

        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        // GET: Users
        public ActionResult Index()
        {

            var source = from f in db.Users
                         select f;

            if (User.IsInRole("Senior Manager"))
            {
                source = source.Where(x=>x.UserGroup.UserGroupName == "School Coordinator");
            }
            else if (User.IsInRole("Director"))
            {
                source = source.Where(x => x.UserGroup.UserGroupName == "Program Leader" || x.UserGroup.UserGroupName  == "Senior Manager");
            }
            else if (User.IsInRole("Head of Teaching Team"))
            {
                source = source.Where(x => x.UserGroup.UserGroupName == "Group Leader");
            }
            return View(source.OrderBy(x => x.Surname).ThenBy(x => x.FirstName).ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {

            if (User.IsInRole("Senior Manager"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1009",Text="School Coordinator"}
                                          }
                                          ,"Value","Text");
            }
            else if (User.IsInRole("Director"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1006",Text="Program Leader"},
                                              new { Value="1008",Text="Senior Manager"}
                                          }
                                          , "Value", "Text");
            }
            else if (User.IsInRole("Head of Teaching Team"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1007",Text="Group Leader"}
                                          }
                                          , "Value", "Text");
            }
            else
            { 
                ViewBag.UserGroupID = new SelectList(db.UserGroups, "UserGroupID", "UserGroupName");
            }
            var source = from f in db.ReportDatas
                         select f;

            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = from f in db.ReportDatas
                             where u.Schools.Contains(f.School)
                             select f;
            }

            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();
                
            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");



            var schools = (from f in source
                           select new { School = f.School }).Distinct();

            ViewBag.Schools = new SelectList(schools.OrderBy(x => x.School), "School", "School");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Hidden_Schools, string Hidden_JobCodes,[Bind(Include = "EmployeeNumber,Schools,JobCodes,UserID,UserGUID,UserGroupID,Email,PWD,FirstName,Surname,CreatedBy,CreatedDate,ModifedBy,ModifiedDate")] User user)
        {
            if (ModelState.IsValid)
            {
                User u = UserHelper.getMember(db);
                user.UserGUID = Guid.NewGuid();
                user.CreatedBy = u.UserID;
                user.CreatedDate = DateTime.Now;
                user.ModifedBy = u.UserID;
                user.ModifiedDate = DateTime.Now;

                if (user.UserGroupID == 1006 || user.UserGroupID == 1007 || user.UserGroupID == 1009)
                {
                    user.JobCodes = Hidden_JobCodes;
                }

                if (user.UserGroupID == 1008)
                {
                    user.Schools = Hidden_Schools;
                }

                string newPWD = Security.CreateRandomPassword(8);
                user.PWD = Security.HashSHA1(newPWD + user.UserGUID.ToString());
                db.Users.Add(user);
                db.SaveChanges();

                Email.SendEmail(user.Email, WebConfigurationManager.AppSettings["EmailFrom"].ToString(), "OEG - Reports User", Email.NewUserEmail(newPWD));
                
                return RedirectToAction("Index");
            }
            if (User.IsInRole("Senior Manager"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1009",Text="School Coordinator"}
                                          }
                                          , "Value", "Text");
            }
            else if (User.IsInRole("Director"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1006",Text="Program Leader"},
                                              new { Value="1008",Text="Senior Manager"}
                                          }
                                          , "Value", "Text");
            }
            else if (User.IsInRole("Head of Teaching Team"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1007",Text="Group Leader"}
                                          }
                                          , "Value", "Text");
            }

            else
            {
                ViewBag.UserGroupID = new SelectList(db.UserGroups, "UserGroupID", "UserGroupName");
            }

            var source = from f in db.ReportDatas
                         select f;

            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = from f in db.ReportDatas
                         where u.Schools.Contains(f.School)
                         select f;
            }

            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");


            var schools = (from f in source
                           select new { School = f.School }).Distinct();

            ViewBag.Schools = new SelectList(schools.OrderBy(x => x.School), "School", "School");

            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            ViewBag.Hidden_Schools = Hidden_Schools;

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Senior Manager"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1009",Text="School Coordinator"}
                                          }
                                          , "Value", "Text", user.UserGroupID);
            }
            else if (User.IsInRole("Director"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1006",Text="Program Leader"},
                                              new { Value="1008",Text="Senior Manager"}
                                          }
                                          , "Value", "Text", user.UserGroupID);
            }
            else if (User.IsInRole("Head of Teaching Team"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1007",Text="Group Leader"}
                                          }
                                          , "Value", "Text", user.UserGroupID);
            }

            else
            {
                ViewBag.UserGroupID = new SelectList(db.UserGroups, "UserGroupID", "UserGroupName", user.UserGroupID);
            }

            var source = from f in db.ReportDatas
                         select f;

            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = from f in db.ReportDatas
                         where u.Schools.Contains(f.School)
                         select f;
            }

            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");


            var schools = (from f in source
                           select new { School = f.School }).Distinct();

            ViewBag.Schools = new SelectList(schools.OrderBy(x => x.School), "School", "School");

            ViewBag.Hidden_JobCodes = user.JobCodes;
            ViewBag.Hidden_Schools = user.Schools;


            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string Hidden_Schools, string Hidden_JobCodes, [Bind(Include = "EmployeeNumber,School,UserID,UserGUID,UserGroupID,Email,PWD,FirstName,Surname,CreatedBy,CreatedDate,ModifedBy,ModifiedDate")] User user)
        {
            if (ModelState.IsValid)
            {
                User u = UserHelper.getMember(db);
                user.ModifedBy = u.UserID;
                user.ModifiedDate = DateTime.Now;

                if (user.UserGroupID == 1006 || user.UserGroupID == 1007 || user.UserGroupID == 1009)
                {
                    user.JobCodes = Hidden_JobCodes;
                }

                if (user.UserGroupID == 1008)
                {
                    user.Schools = Hidden_Schools;
                }
                
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           

            if (User.IsInRole("Senior Manager"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1009",Text="School Coordinator"}
                                          }
                                          , "Value", "Text", user.UserGroupID);
            }
            else if (User.IsInRole("Director"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1006",Text="Program Leader"},
                                              new { Value="1008",Text="Senior Manager"}
                                          }
                                          , "Value", "Text", user.UserGroupID);
            }
            else if (User.IsInRole("Head of Teaching Team"))
            {
                ViewBag.UserGroupID = new SelectList(new[]
                                          {
                                              new { Value="1007",Text="Group Leader"}
                                          }
                                          , "Value", "Text", user.UserGroupID);
            }

            else
            {
                ViewBag.UserGroupID = new SelectList(db.UserGroups, "UserGroupID", "UserGroupName", user.UserGroupID);
            }

            var source = from f in db.ReportDatas
                         select f;

            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = from f in db.ReportDatas
                         where u.Schools.Contains(f.School)
                         select f;
            }


            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");


            var schools = (from f in source
                           select new { School = f.School }).Distinct();

            ViewBag.Schools = new SelectList(schools.OrderBy(x => x.School), "School", "School");

            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            ViewBag.Hidden_Schools = Hidden_Schools;



            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "email, password, rememberme")] PasswordSignUp signup, string returnUrl)
        {
            User u = new User();
            if (ModelState.IsValid)
            {
                try
                {
                    //find active member with supplied email
                    u = (from a in db.Users
                                where a.Email == signup.email
                                select a).FirstOrDefault();
                    //if active member found
                    if (u != null)
                    {
                        //validate the suplied password
                        if (u.PWD == Security.HashSHA1(signup.password + u.UserGUID.ToString()))
                        //if (u.PWD == signup.password)
                        {
                            //we got a valid login
                            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, signup.email), }, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Email, ClaimTypes.Role);

                            // if you want roles, just add as many as you want here (for loop maybe?)
                            identity.AddClaim(new Claim(ClaimTypes.Role, u.UserGroup.UserGroupName));
                            // tell OWIN the identity provider, optional
                            // identity.AddClaim(new Claim(IdentityProvider, "Simplest Auth"));

                            Authentication.SignIn(new AuthenticationProperties { IsPersistent = signup.rememberme }, identity);

                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            //valid user, wrong password
                            ViewData["Message"] = new MyMessage(WebConfigurationManager.AppSettings["LoginFail"].ToString(), Importance.Error);

                        }
                    }
                    else
                    {
                        //invalid user
                        ViewData["Message"] = new MyMessage(WebConfigurationManager.AppSettings["LoginFail"].ToString(), Importance.Error);

                    }
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = new MyMessage(WebConfigurationManager.AppSettings["GenericError"].ToString(), Importance.Error);
                }
            }

            return View(signup);
        }


        public ActionResult Logout()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("LogIn");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword([Bind(Include = "email")] MinSignUp signup)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    User u = (from a in db.Users
                                     where a.Email == signup.email
                                     select a).FirstOrDefault();

                    if (u != null)
                    {
                        string newPWD = Security.CreateRandomPassword(8);
                        u.PWD = Security.HashSHA1(newPWD + u.UserGUID.ToString());
                        u.ModifiedDate = DateTime.Now;
                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();

                        Email.SendEmail(signup.email, WebConfigurationManager.AppSettings["EmailFrom"].ToString(), "OEG - Password Reset", Email.ForgotPasswordEmail(newPWD));

                        //ok, so database is happy, let send this cat to mailchimp for later chimpyness
                        ViewData["Message"] = new MyMessage(WebConfigurationManager.AppSettings["ForgotPasswordMessage"].ToString(), Importance.Success);
                    }
                    else
                    {
                        ViewData["Message"] = new MyMessage(@"Invalid email.", Importance.Warning);
                    }
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

                catch (Exception ex)
                {
                    ViewData["Message"] = new MyMessage(WebConfigurationManager.AppSettings["GenericError"].ToString(), Importance.Error);
                }
            }

            return View(signup);
        }

        public ActionResult ResetPassword(string email)
        {

            try
            {
                User u = (from a in db.Users
                          where a.Email == email
                          select a).FirstOrDefault();

                if (u != null)
                {
                    string newPWD = Security.CreateRandomPassword(8);
                    u.PWD = Security.HashSHA1(newPWD + u.UserGUID.ToString());
                    u.ModifiedDate = DateTime.Now;
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();

                    Email.SendEmail(email, WebConfigurationManager.AppSettings["EmailFrom"].ToString(), "OEG - Password Reset", Email.ForgotPasswordEmail(newPWD));

                    //ok, so database is happy,
                    ViewData["Message"] = new MyMessage(WebConfigurationManager.AppSettings["ResetPasswordMessage"].ToString(), Importance.Success);
                }
                else
                {
                    ViewData["Message"] = new MyMessage(@"Invalid email.", Importance.Warning);
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            catch (Exception ex)
            {
                ViewData["Message"] = new MyMessage(WebConfigurationManager.AppSettings["GenericError"].ToString(), Importance.Error);
            }
            return View();
        }
        

        public ActionResult ChangePassword()
        {
            ChangePassword cp = new ChangePassword();
            return View(cp);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "NewPassword, ConfirmPassword")] ChangePassword cp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User u = UserHelper.getMember(db);
                    u.PWD = Security.HashSHA1(cp.NewPassword + u.UserGUID.ToString());
                    u.ModifiedDate = DateTime.Now;
                    u.ModifedBy = u.UserID;
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewData["Message"] = new MyMessage("Your password has been updated", Importance.Success);
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = new MyMessage("There was a problem updating your password.", Importance.Error);
                }

            }

            return View(cp);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Reports");
            }
        }

        public JsonResult GetEmployeeNumber(string firstname, string surname)
        {
            oeg_lookupsEntities db2 = new oeg_lookupsEntities();
            var retset = db2.getEmployeeIDByName(firstname, surname).ToList();
            string ret = retset.Count() > 0 ? retset.FirstOrDefault().EntityID.ToString() : "No Match Found!"; 

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

    }
}
