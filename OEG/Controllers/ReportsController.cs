﻿using OEG.Models;
using OEG.Static_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OEG.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {

        private oeg_reportsEntities db = new oeg_reportsEntities();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        
        [Authorize(Roles = "Administrator")]
        public ActionResult AllData()
        {
            return View(db.ReportDatas.ToList());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult StaffData()
        {
            return View(db.StaffReportData.ToList());
        }

        public ActionResult ProgramsBenchmark()
        {
            var factors = (from f in db.ReportDatas
                           select new { Factor = f.Factor }).Distinct();

            ViewBag.Factors = new SelectList(factors.OrderBy(x=>x.Factor), "Factor", "Factor");
            return View(db.ProgramsBenchmark(null).ToList());
        }

        [HttpPost]
        public ActionResult ProgramsBenchmark(string Hidden_Factors)
        {
            var factors = (from f in db.ReportDatas
                           select new { Factor = f.Factor }).Distinct();

            ViewBag.Factors = new SelectList(factors.OrderBy(x => x.Factor), "Factor", "Factor");
            ViewBag.Hidden_Factors = Hidden_Factors;
            return View(db.ProgramsBenchmark(Hidden_Factors.Length > 0 ? Hidden_Factors : null ).ToList());
        }

        public ActionResult JobCodePrograms()
        {
            var factors = (from f in db.ReportDatas
                           select new { Factor = f.Factor }).Distinct();

            ViewBag.Factors = new SelectList(factors.OrderBy(x => x.Factor), "Factor", "Factor");
            return View(db.JobCodePrograms(null).ToList());
        }

        [HttpPost]
        public ActionResult JobCodePrograms(string Hidden_Factors)
        {
            var factors = (from f in db.ReportDatas
                           select new { Factor = f.Factor }).Distinct();

            ViewBag.Factors = new SelectList(factors.OrderBy(x => x.Factor), "Factor", "Factor");
            ViewBag.Hidden_Factors = Hidden_Factors;
            return View(db.JobCodePrograms(Hidden_Factors.Length > 0 ? Hidden_Factors : null).ToList());
        }


        public ActionResult GroupsByEmployee()
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("School Coordinator"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader")) EmpNo = u.EmployeeNumber;
                ViewBag.Hidden_Employees = EmpNo;
            }
            else if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }
            else
            {
                var employees = (from f in source
                                 where f.EmployeeName != null
                                 select new { EmployeeNumber = f.EmployeeNumber, EmployeeName = f.EmployeeName }).Distinct();

                ViewBag.Employees = new SelectList(employees.OrderBy(x => x.EmployeeName), "EmployeeNumber", "EmployeeName");
            }

            GroupsByEmployeeViewModel vm = new GroupsByEmployeeViewModel();
            vm.ReportData = db.GroupsByEmployee(EmpNo).ToList();
            vm.SubTotal = db.GroupsByEmployeeSubTotal(EmpNo).FirstOrDefault();
 

            return View(vm);
        }

        [HttpPost]
        public ActionResult GroupsByEmployee(string Hidden_Employees)
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader")  || User.IsInRole("School Coordinator"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader")) EmpNo = u.EmployeeNumber;
                ViewBag.Hidden_Employees = EmpNo;
            }
            else if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }
            else
            {
                var employees = (from f in source
                                 where f.EmployeeName != null
                                 select new { EmployeeNumber = f.EmployeeNumber, EmployeeName = f.EmployeeName }).Distinct();

                ViewBag.Employees = new SelectList(employees.OrderBy(x => x.EmployeeName), "EmployeeNumber", "EmployeeName");

                ViewBag.Hidden_Employees = Hidden_Employees;
            }

            GroupsByEmployeeViewModel vm = new GroupsByEmployeeViewModel();
            vm.ReportData = db.GroupsByEmployee(Hidden_Employees.Length > 0 ? Hidden_Employees : null).ToList();
            vm.SubTotal = db.GroupsByEmployeeSubTotal(Hidden_Employees.Length > 0 ? Hidden_Employees : null).FirstOrDefault();


            return View(vm);
        }


        public ActionResult DurationBenchmark()
        {
            var days = (from f in db.ReportDatas
                           select new { Days = f.Days }).Distinct();

            ViewBag.Days = new SelectList(days.OrderBy(x => x.Days), "Days", "Days");

            return View(db.DurationBenchmark(null).ToList());
        }


        [HttpPost]
        public ActionResult DurationBenchmark(string Hidden_Days)
        {
            var days = (from f in db.ReportDatas
                        select new { Days = f.Days }).Distinct();

            ViewBag.Days = new SelectList(days.OrderBy(x => x.Days), "Days", "Days");
            ViewBag.Hidden_Days = Hidden_Days;
            return View(db.DurationBenchmark(Hidden_Days.Length > 0 ? Hidden_Days : null).ToList());
        }

        public ActionResult Duration()
        {
            var days = (from f in db.ReportDatas
                        where f.Days != null
                        select new { Days = f.Days }).Distinct();

            ViewBag.Days = new SelectList(days.OrderBy(x => x.Days), "Days", "Days");

            return View(db.Duration(null).ToList());
        }


        [HttpPost]
        public ActionResult Duration(string Hidden_Days)
        {
            var days = (from f in db.ReportDatas
                        where f.Days != null
                        select new { Days = f.Days }).Distinct();

            ViewBag.Days = new SelectList(days.OrderBy(x => x.Days), "Days", "Days");
            ViewBag.Hidden_Days = Hidden_Days;
            return View(db.Duration(Hidden_Days.Length > 0 ? Hidden_Days : null).ToList());
        }


        public ActionResult Venue()
        {
            return View(db.Venue().ToList());
        }

        public ActionResult School()
        {
            return View(db.School().ToList());
        }

        public ActionResult Item()
        {
            return View(db.Item().ToList());
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult StandardDeviation()
        {
            List<StdDevByFactor_Result> Factor = db.StdDevByFactor().ToList();
            List<StdDevByQuestionID_Result> Question = db.StdDevByQuestionID().ToList();

            StdDevViewModel vm = new StdDevViewModel();
            vm.Factor = Factor;
            vm.Question = Question;

            return View(vm);
        }

        public ActionResult QuantitativeByGroup()
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") )
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader")) EmpNo = u.EmployeeNumber;
            }
            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }

            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            string ret = "";
            foreach(string s in jobcodes.Select(o=>o.JobCode))
            {
                ret += s + ",";
            }

            if (ret.Length > 0) ret = ret.Remove(ret.Length - 1);
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                ViewBag.Hidden_JobCodes = ret;
            }
            return View(db.QuantativeByGroup(ret, EmpNo).ToList());
        }

        [HttpPost]
        public ActionResult QuantitativeByGroup(string Hidden_JobCodes)
        {
            
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;
            
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader")  )
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader")) EmpNo = u.EmployeeNumber;
            }
            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }


            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                if (Hidden_JobCodes.Length == 0)
                {
                    string ret = "";
                    foreach (string s in jobcodes.Select(o => o.JobCode))
                    {
                        ret += s + ",";
                    }

                    if (ret.Length > 0) ret = ret.Remove(ret.Length - 1);
                    Hidden_JobCodes = ret;
                }
            }
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            return View(db.QuantativeByGroup(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null, EmpNo).ToList());
        }


        public ActionResult JobCodeGroup()
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader"))
                {
                    EmpNo = u.EmployeeNumber;
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber);

                    var groupcodes = (from f in staffdata
                                      select new { JobCode = f.JobCode }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.JobCode))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => gc.Contains(x.JobCode));
                }
            }

            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }

            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            string jc = "";
            foreach (string s in jobcodes.Select(o => o.JobCode))
            {
                jc += s + ",";
            }

            if (jc.Length > 0) jc = jc.Remove(jc.Length - 1);
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                ViewBag.Hidden_JobCodes = jc;
            }

            if (User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                //GET GROUPS FROM STAFFDATA
                var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x=> jc.Contains(x.JobCode));

                var groupcodes = (from f in staffdata
                             select new { Group = f.Group }).Distinct();

                string gc = "";
                foreach (string s in groupcodes.Select(o => o.Group))
                {
                    gc += s + ",";
                }

                if (gc.Length > 0) gc = gc.Remove(gc.Length - 1); 

                source = source.Where(x => x.Group.Contains(gc));
            }

            var groups = (from f in source
                            select new { Group = f.Group }).Distinct();

            ViewBag.Groups = new SelectList(groups.OrderBy(x => x.Group), "Group", "Group");

            string g = "";
            foreach (string s in groups.Select(o => o.Group))
            {
                g += s + ",";
            }

            if (g.Length>0) g = g.Remove(g.Length - 1);
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                ViewBag.Hidden_Groups = g;
            }

            JobCodeGroupViewModel vm = new JobCodeGroupViewModel();
            vm.ReportData = db.JobCodeGroup(jc, g, EmpNo).ToList();
            vm.SubTotal = db.JobCodeGroupSubTotal(jc, g, EmpNo).FirstOrDefault();


            return View(vm);
        }

        [HttpPost]
        public ActionResult JobCodeGroup(string Hidden_JobCodes, string Hidden_Groups)
        {

            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") )
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader"))
                {
                    EmpNo = u.EmployeeNumber;
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber);

                    var groupcodes = (from f in staffdata
                                      select new { JobCode = f.JobCode }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.JobCode))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => gc.Contains(x.JobCode));
                }
            }

            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }


            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            string ret = "";
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                if (Hidden_JobCodes.Length == 0)
                {
                    
                    foreach (string s in jobcodes.Select(o => o.JobCode))
                    {
                        ret += s + ",";
                    }

                    if (ret.Length > 0) ret = ret.Remove(ret.Length - 1);
                    Hidden_JobCodes = ret;
                }
            }
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;


            if (User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                //GET GROUPS FROM STAFFDATA
                var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x => ret.Contains(x.JobCode));

                var groupcodes = (from f in staffdata
                                  select new { Group = f.Group }).Distinct();

                string gc = "";
                foreach (string s in groupcodes.Select(o => o.Group))
                {
                    gc += s + ",";
                }

                if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                source = source.Where(x => x.Group.Contains(gc));
            }


            var groups = (from f in source
                          select new { Group = f.Group }).Distinct();

            ViewBag.Groups = new SelectList(groups.OrderBy(x => x.Group), "Group", "Group");


            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                if (Hidden_Groups.Length == 0)
                {
                    string ret1 = "";
                    foreach (string s in groups.Select(o => o.Group))
                    {
                        ret1 += s + ",";
                    }

                    if (ret1.Length > 0) ret1 = ret1.Remove(ret.Length - 1);
                    Hidden_Groups = ret1;
                }
            }

            ViewBag.Hidden_Groups = Hidden_Groups;

            JobCodeGroupViewModel vm = new JobCodeGroupViewModel();
            vm.ReportData = db.JobCodeGroup(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null, Hidden_Groups.Length > 0 ? Hidden_Groups : null, EmpNo).ToList();
            vm.SubTotal = db.JobCodeGroupSubTotal(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null, Hidden_Groups.Length > 0 ? Hidden_Groups : null, EmpNo).FirstOrDefault();

            return View(vm);
        }



        public ActionResult AllCoursesBenchmark()
        {

            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            return View(db.AllCoursesBenchmark(null).ToList());
        }

        [HttpPost]
        public ActionResult AllCoursesBenchmark(string Hidden_JobCodes)
        {

            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            return View(db.AllCoursesBenchmark(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null).ToList());
        }

        public ActionResult YearLevelCompetency()
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            return View(db.YearLevelBenchmark(null).ToList());
        }

        [HttpPost]
        public ActionResult YearLevelCompetency(string Hidden_JobCodes)
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            return View(db.YearLevelBenchmark(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null).ToList());
        }


        public ActionResult Competency()
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            return View(db.Competency(null).ToList());
        }

        [HttpPost]
        public ActionResult Competency(string Hidden_JobCodes)
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            return View(db.Competency(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null).ToList());
        }


        public ActionResult YearLevel()
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            return View(db.YearLevel(null).ToList());
        }

        [HttpPost]
        public ActionResult YearLevel(string Hidden_JobCodes)
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            return View(db.YearLevel(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null).ToList());
        }




        public ActionResult ItemJobCode()
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") ||  User.IsInRole("School Coordinator"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader"))
                {
                    EmpNo = u.EmployeeNumber;
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber);

                    var groupcodes = (from f in staffdata
                                      select new { JobCode = f.JobCode }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.JobCode))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => gc.Contains(x.JobCode));
                }
                ViewBag.Hidden_Employees = EmpNo;
            }
            else if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }

            else
            {
                var employees = (from f in source
                                 where f.EmployeeName != null
                                 select new { EmployeeNumber = f.EmployeeNumber, EmployeeName = f.EmployeeName }).Distinct();

                ViewBag.Employees = new SelectList(employees.OrderBy(x => x.EmployeeName), "EmployeeNumber", "EmployeeName");
            }

            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            string ret = "";
            foreach (string s in jobcodes.Select(o => o.JobCode))
            {
                ret += s + ",";
            }

            if (ret.Length > 0) ret = ret.Remove(ret.Length - 1);
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager")  || User.IsInRole("School Coordinator"))
            {
                ViewBag.Hidden_JobCodes = ret;
            }

            if (User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                //GET GROUPS FROM STAFFDATA
                var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x => ret.Contains(x.JobCode));

                var groupcodes = (from f in staffdata
                                  select new { Group = f.Group }).Distinct();

                string gc = "";
                foreach (string s in groupcodes.Select(o => o.Group))
                {
                    gc += s + ",";
                }

                if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                source = source.Where(x => x.Group.Contains(gc));
            }


            var groups = (from f in source
                          select new { Group = f.Group }).Distinct();

            ViewBag.Groups = new SelectList(groups.OrderBy(x => x.Group), "Group", "Group");

            string g = null;
            foreach (string s in groups.Select(o => o.Group))
            {
                g += s + ",";
            }

            if (g.Length > 0) g = g.Remove(g.Length - 1);
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager")  || User.IsInRole("School Coordinator"))
            {
                ViewBag.Hidden_Groups = g;
            }


            ItemJobCodeViewModel vm = new ItemJobCodeViewModel();
            vm.ReportData = db.ItemJobCode(ret, g, EmpNo).ToList();
            vm.SubTotal = db.ItemJobCodeSubTotal(ret, g, EmpNo).FirstOrDefault();

            return View(vm);
        }

        [HttpPost]
        public ActionResult ItemJobCode(string Hidden_JobCodes, string Hidden_Groups, string Hidden_Employees)
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("School Coordinator"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader"))
                {
                    EmpNo = u.EmployeeNumber;
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber);

                    var groupcodes = (from f in staffdata
                                      select new { JobCode = f.JobCode }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.JobCode))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => gc.Contains(x.JobCode));
                }

                ViewBag.Hidden_Employees = EmpNo;
            }
            else if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }
            else
            {
                var employees = (from f in source
                                 where f.EmployeeName != null
                                 select new { EmployeeNumber = f.EmployeeNumber, EmployeeName = f.EmployeeName }).Distinct();

                ViewBag.Employees = new SelectList(employees.OrderBy(x => x.EmployeeName), "EmployeeNumber", "EmployeeName");

                ViewBag.Hidden_Employees = Hidden_Employees;
            }


            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            string ret = "";
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager")  || User.IsInRole("School Coordinator"))
            {
                if (Hidden_JobCodes.Length == 0)
                {
                    
                    foreach (string s in jobcodes.Select(o => o.JobCode))
                    {
                        ret += s + ",";
                    }

                    if (ret.Length > 0) ret = ret.Remove(ret.Length - 1);
                    Hidden_JobCodes = ret;
                }
            }

            ViewBag.Hidden_JobCodes = Hidden_JobCodes;

            if (User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                //GET GROUPS FROM STAFFDATA
                var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x => ret.Contains(x.JobCode));

                var groupcodes = (from f in staffdata
                                  select new { Group = f.Group }).Distinct();

                string gc = "";
                foreach (string s in groupcodes.Select(o => o.Group))
                {
                    gc += s + ",";
                }

                if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                source = source.Where(x => x.Group.Contains(gc));
            }


            var groups = (from f in source
                          select new { Group = f.Group }).Distinct();

            ViewBag.Groups = new SelectList(groups.OrderBy(x => x.Group), "Group", "Group");


            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager")  || User.IsInRole("School Coordinator"))
            {
                if (Hidden_Groups.Length == 0)
                {
                    string ret1 = "";
                    foreach (string s in groups.Select(o => o.Group))
                    {
                        ret1 += s + ",";
                    }

                    if (ret1.Length > 0)  ret1 = ret1.Remove(ret1.Length - 1);
                    Hidden_Groups = ret1;
                }
            }

            ViewBag.Hidden_Groups = Hidden_Groups;

            ItemJobCodeViewModel vm = new ItemJobCodeViewModel();
            vm.ReportData = db.ItemJobCode(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null, Hidden_Groups.Length > 0 ? Hidden_Groups : null, Hidden_Employees.Length > 0 ? Hidden_Employees : EmpNo).ToList();
            vm.SubTotal = db.ItemJobCodeSubTotal(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null, Hidden_Groups.Length > 0 ? Hidden_Groups : null, Hidden_Employees.Length > 0 ? Hidden_Employees : EmpNo).FirstOrDefault();

            return View(vm);
        }




        public ActionResult SchoolQuantitativeByGroup()
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") ||  User.IsInRole("School Coordinator"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader"))
                {
                    EmpNo = u.EmployeeNumber;
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber);

                    var groupcodes = (from f in staffdata
                                      select new { JobCode = f.JobCode }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.JobCode))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => gc.Contains(x.JobCode));
                }
            }
            else if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }
            else
            {
                var employees = (from f in source
                                 where f.EmployeeName != null
                                 select new { EmployeeNumber = f.EmployeeNumber, EmployeeName = f.EmployeeName }).Distinct();

                ViewBag.Employees = new SelectList(employees.OrderBy(x => x.EmployeeName), "EmployeeNumber", "EmployeeName");
            }


            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            string ret = "";
            foreach (string s in jobcodes.Select(o => o.JobCode))
            {
                ret += s + ",";
            }

            if (ret.Length > 0) ret = ret.Remove(ret.Length - 1);
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                ViewBag.Hidden_JobCodes = ret;
            }

            if (User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                //GET GROUPS FROM STAFFDATA
                var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x => ret.Contains(x.JobCode));

                var groupcodes = (from f in staffdata
                                  select new { Group = f.Group }).Distinct();

                string gc = "";
                foreach (string s in groupcodes.Select(o => o.Group))
                {
                    gc += s + ",";
                }

                if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                source = source.Where(x => x.Group.Contains(gc));
            }



            var groups = (from f in source
                          select new { Group = f.Group }).Distinct();

            ViewBag.Groups = new SelectList(groups.OrderBy(x => x.Group), "Group", "Group");

            string g = "";
            foreach (string s in groups.Select(o => o.Group))
            {
                g += s + ",";
            }

            if (g.Length > 0) g = g.Remove(g.Length - 1);
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                ViewBag.Hidden_Groups = g;
            }

            SchoolQuantitativeByGroupViewModel vm = new SchoolQuantitativeByGroupViewModel();
            vm.ReportData = db.SchoolQuantativeByGroup(ret, g, EmpNo).ToList();
            vm.SubTotal = db.SchoolQuantativeByGroupSubTotal(ret, g, EmpNo).FirstOrDefault();

            return View(vm);
        }

        [HttpPost]
        public ActionResult SchoolQuantitativeByGroup(string Hidden_JobCodes, string Hidden_Groups, string Hidden_Employees)
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = Hidden_Employees.Length > 0 ? Hidden_Employees : null;

            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") ||  User.IsInRole("School Coordinator"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader"))
                {
                    EmpNo = u.EmployeeNumber;
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber);

                    var groupcodes = (from f in staffdata
                                      select new { JobCode = f.JobCode }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.JobCode))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => gc.Contains(x.JobCode));
                }
            }
            else if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }
            else
            {
                var employees = (from f in source
                                 where f.EmployeeName != null
                                 select new { EmployeeNumber = f.EmployeeNumber, EmployeeName = f.EmployeeName }).Distinct();

                ViewBag.Employees = new SelectList(employees.OrderBy(x => x.EmployeeName), "EmployeeNumber", "EmployeeName");

                ViewBag.Hidden_Employees = Hidden_Employees;
            }



            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            string ret = "";
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                if (Hidden_JobCodes.Length == 0)
                {
                    
                    foreach (string s in jobcodes.Select(o => o.JobCode))
                    {
                        ret += s + ",";
                    }

                    if (ret.Length > 0) ret = ret.Remove(ret.Length - 1);
                    Hidden_JobCodes = ret;
                }
            }

            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            if (User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                //GET GROUPS FROM STAFFDATA
                var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x => ret.Contains(x.JobCode));

                var groupcodes = (from f in staffdata
                                  select new { Group = f.Group }).Distinct();

                string gc = "";
                foreach (string s in groupcodes.Select(o => o.Group))
                {
                    gc += s + ",";
                }

                if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                source = source.Where(x => x.Group.Contains(gc));
            }



            var groups = (from f in source
                          select new { Group = f.Group }).Distinct();

            ViewBag.Groups = new SelectList(groups.OrderBy(x => x.Group), "Group", "Group");


            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                if (Hidden_Groups.Length == 0)
                {
                    string ret1 = "";
                    foreach (string s in groups.Select(o => o.Group))
                    {
                        ret1 += s + ",";
                    }

                    if (ret1.Length > 0) ret1 = ret1.Remove(ret1.Length - 1);
                    Hidden_Groups = ret1;
                }
            }

            ViewBag.Hidden_Groups = Hidden_Groups;

            SchoolQuantitativeByGroupViewModel vm = new SchoolQuantitativeByGroupViewModel();
            vm.ReportData = db.SchoolQuantativeByGroup(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null, Hidden_Groups.Length > 0 ? Hidden_Groups : null, EmpNo).ToList();
            vm.SubTotal = db.SchoolQuantativeByGroupSubTotal(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null, Hidden_Groups.Length > 0 ? Hidden_Groups : null, EmpNo).FirstOrDefault();

            return View(vm);
        }


        public ActionResult SchoolQualitative()
        {
            var source = from f in db.ReportDatas
                         select f;

            string EmpNo = null;
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") ||  User.IsInRole("School Coordinator"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader"))
                {
                    EmpNo = u.EmployeeNumber;
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber);

                    var groupcodes = (from f in staffdata
                                      select new { JobCode = f.JobCode }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.JobCode))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => gc.Contains(x.JobCode));
                    
                }
            }
            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }

            
            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            string jc = "";
            foreach (string s in jobcodes.Select(o => o.JobCode))
            {
                jc += s + ",";
            }
            if (jc.Length > 0) jc = jc.Remove(jc.Length - 1);


            if (User.IsInRole("Group Leader"))
            {
                ViewBag.Hidden_JobCodes = jc;
            }

            var schools = (from f in source
                           where f.School != null
                            select new { School = f.School }).Distinct();

            string sc = "";
            foreach (string s in schools.Select(o => o.School))
            {
                sc += s + ",";
            }
            if (sc.Length > 0) sc = sc.Remove(sc.Length - 1);


            ViewBag.Schools = new SelectList(schools.OrderBy(x => x.School), "School", "School");
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager") || User.IsInRole("School Coordinator"))
            {
                ViewBag.Hidden_Schools = sc;
            }
            var years = (from f in source
                         where f.Year != null
                            select new { Year = f.Year}).Distinct();

            ViewBag.Years = new SelectList(years.OrderBy(x => x.Year), "Year", "Year");
            string y = "";
            foreach (int s in years.Select(o => o.Year))
            {
                y += s.ToString() + ",";
            }
            if (y.Length > 0) y = y.Remove(y.Length - 1);

            var venues = (from f in source
                          where f.Venue != null
                          select new { Venue = f.Venue }).Distinct();

            ViewBag.Venues = new SelectList(venues.OrderBy(x => x.Venue), "Venue", "Venue");
            string v = "";
            foreach (string s in venues.Select(o => o.Venue))
            {
                v += s + ",";
            }
            if (v.Length > 0) v = v.Remove(v.Length - 1);


            var startdates = source.OrderBy(d => d.ProgramStartDate)
                .Select(d => d.ProgramStartDate)
                .AsEnumerable()
                .Select(date => date.GetValueOrDefault().ToString("dd/MM/yyyy"))
                .Distinct()
                .Select(StartDate => new SelectListItem { Text = StartDate, Value = StartDate })
                .ToList();

            ViewBag.StartDates = new SelectList(startdates, "Text", "Value");
            string sd = "";
            foreach (string s in startdates.Select(o => o.Text))
            {
                sd += s + ",";
            }
            if (sd.Length > 0) sd = sd.Remove(sd.Length - 1);


            if (User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                //GET GROUPS FROM STAFFDATA
                var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x => jc.Contains(x.JobCode));

                var groupcodes = (from f in staffdata
                                  select new { Group = f.Group }).Distinct();

                string gc = "";
                foreach (string s in groupcodes.Select(o => o.Group))
                {
                    gc += s + ",";
                }

                if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                source = source.Where(x => x.Group.Contains(gc));
            }


            var groups = (from f in source
                          select new { Group = f.Group }).Distinct();

            ViewBag.Groups = new SelectList(groups.OrderBy(x => x.Group), "Group", "Group");

            string g = "";
            foreach (string s in groups.Select(o => o.Group))
            {
                g += s + ",";
            }

            if (g.Length > 0) g = g.Remove(g.Length - 1);
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                ViewBag.Hidden_Groups = g;
            }


            return View(db.SchoolQualative(y, sc, jc,g, v, sd, EmpNo).ToList());
        }

        [HttpPost]
        public ActionResult SchoolQualitative(string Hidden_Years, string Hidden_Schools, string Hidden_JobCodes, string Hidden_Venues, string Hidden_StartDates, string Hidden_Groups)
        {

            var source = from f in db.ReportDatas
                         select f;
            string EmpNo = null;
            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("School Coordinator"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.JobCodes.Contains(x.JobCode));
                if (User.IsInRole("Group Leader"))
                {
                    EmpNo = u.EmployeeNumber;
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber);

                    var groupcodes = (from f in staffdata
                                      select new { JobCode = f.JobCode }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.JobCode))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => gc.Contains(x.JobCode));

                }
            }
            if (User.IsInRole("Senior Manager"))
            {
                User u = UserHelper.getMember(db);
                source = source.Where(x => u.Schools.Contains(x.School));
            }

            var jobcodes = (from f in source
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;

            var schools = (from f in source
                           where f.School != null
                           select new { School = f.School }).Distinct();

            ViewBag.Schools = new SelectList(schools.OrderBy(x => x.School), "School", "School");
            ViewBag.Hidden_Schools = Hidden_Schools;

            var years = (from f in source
                         where f.Year != null
                         select new { Year = f.Year }).Distinct();

            ViewBag.Years = new SelectList(years.OrderBy(x => x.Year), "Year", "Year");
            ViewBag.Hidden_Years = Hidden_Years;

            var venues = (from f in source
                          where f.Venue != null
                          select new { Venue = f.Venue }).Distinct();

            ViewBag.Venues = new SelectList(venues.OrderBy(x => x.Venue), "Venue", "Venue");
            ViewBag.Hidden_Venues = Hidden_Venues;

            var startdates = source.OrderBy(d => d.ProgramStartDate)
                .Select(d => d.ProgramStartDate)
                .AsEnumerable()
                .Select(date => date.GetValueOrDefault().ToString("dd/MM/yyyy"))
                .Distinct()
                .Select(StartDate => new SelectListItem { Text = StartDate, Value = StartDate })
                .ToList();

            ViewBag.StartDates = new SelectList(startdates, "Text", "Value");
            ViewBag.Hidden_StartDates = Hidden_StartDates;

            if (User.IsInRole("Group Leader"))
            {
                User u = UserHelper.getMember(db);
                //GET GROUPS FROM STAFFDATA
                var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x => Hidden_JobCodes.Contains(x.JobCode));

                var groupcodes = (from f in staffdata
                                  select new { Group = f.Group }).Distinct();

                string gc = "";
                foreach (string s in groupcodes.Select(o => o.Group))
                {
                    gc += s + ",";
                }

                if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                source = source.Where(x => x.Group.Contains(gc));
            }


            var groups = (from f in source
                          select new { Group = f.Group }).Distinct();

            ViewBag.Groups = new SelectList(groups.OrderBy(x => x.Group), "Group", "Group");


            if (User.IsInRole("Program Leader") || User.IsInRole("Group Leader") || User.IsInRole("Senior Manager"))
            {
                if (Hidden_Groups.Length == 0)
                {
                    string ret = "";
                    foreach (string s in groups.Select(o => o.Group))
                    {
                        ret += s + ",";
                    }

                    if (ret.Length > 0) ret = ret.Remove(ret.Length - 1);
                    Hidden_Groups = ret;
                }
            }

            ViewBag.Hidden_Groups = Hidden_Groups;


            return View(db.SchoolQualative(Hidden_Years.Length > 0 ? Hidden_Years : null, Hidden_Schools.Length > 0 ? Hidden_Schools : null, Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null, Hidden_Groups.Length > 0 ? Hidden_Groups : null, Hidden_Venues.Length > 0 ? Hidden_Venues : null, Hidden_StartDates.Length > 0 ? Hidden_StartDates : null, EmpNo).ToList());
        }

        public JsonResult GetGroups(string options)
        {

            var source = from f in db.ReportDatas
                         select f;

            if (options.Length > 0)
            {
                string[] jobCodes = options.Split(',');

                source = source.Where(x => jobCodes.Contains(x.JobCode));

                if (User.IsInRole("Group Leader"))
                {
                    User u = UserHelper.getMember(db);
                    //GET GROUPS FROM STAFFDATA
                    var staffdata = db.StaffReportData.Where(x => x.EmployeeNumber == u.EmployeeNumber).Where(x => options.Contains(x.JobCode));

                    var groupcodes = (from f in staffdata
                                      select new { Group = f.Group }).Distinct();

                    string gc = "";
                    foreach (string s in groupcodes.Select(o => o.Group))
                    {
                        gc += s + ",";
                    }

                    if (gc.Length > 0) gc = gc.Remove(gc.Length - 1);

                    source = source.Where(x => x.Group.Contains(gc));
                }
            }

            var groups = (from f in source
                          select new { Group = f.Group }).Distinct();
            
            var results =
                    from e in groups
                    orderby e.Group
                    select new
                    {
                        id = e.Group,
                        text = e.Group
                    };
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}