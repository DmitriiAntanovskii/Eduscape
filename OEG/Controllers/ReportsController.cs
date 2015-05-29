using OEG.Models;
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

        public ActionResult AllData()
        {
            return View(db.ReportDatas.ToList());
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



        public ActionResult StandardDeviation()
        {
            List<StdDevByFactor_Result> Factor = db.StdDevByFactor().ToList();
            List<StdDevByQuestionID_Result> Question = db.StdDevByQuestionID().ToList();

            StdDevViewModel vm = new StdDevViewModel();
            vm.Factor = Factor;
            vm.Question = Question;

            return View(vm);
        }

        public ActionResult QuantativeByGroup()
        {
            var jobcodes = (from f in db.ReportDatas
                           select new { JobCode = f.JobCode}).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            return View(db.QuantativeByGroup(null).ToList());
        }

        [HttpPost]
        public ActionResult QuantativeByGroup(string Hidden_JobCodes)
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            return View(db.QuantativeByGroup(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null).ToList());
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

        public ActionResult YearLevelBenchmark()
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            return View(db.YearLevelBenchmark(null).ToList());
        }

        [HttpPost]
        public ActionResult YearLevelBenchmark(string Hidden_JobCodes)
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            return View(db.YearLevelBenchmark(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null).ToList());
        }

        public ActionResult SchoolQuantativeByGroup()
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            return View(db.SchoolQuantativeByGroup(null).ToList());
        }

        [HttpPost]
        public ActionResult SchoolQuantativeByGroup(string Hidden_JobCodes)
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;

            return View(db.SchoolQuantativeByGroup(Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null).ToList());
        }


        public ActionResult SchoolQualative()
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");

            var schools = (from f in db.ReportDatas
                            select new { School = f.School }).Distinct();

            ViewBag.Schools = new SelectList(schools.OrderBy(x => x.School), "School", "School");

            var years = (from f in db.ReportDatas
                            select new { Year = f.Year}).Distinct();

            ViewBag.Years = new SelectList(years.OrderBy(x => x.Year), "Year", "Year");

            return View(db.SchoolQualative(null,null,null).ToList());
        }

        [HttpPost]
        public ActionResult SchoolQualative(string Hidden_Years, string Hidden_Schools, string Hidden_JobCodes)
        {
            var jobcodes = (from f in db.ReportDatas
                            select new { JobCode = f.JobCode }).Distinct();

            ViewBag.JobCodes = new SelectList(jobcodes.OrderBy(x => x.JobCode), "JobCode", "JobCode");
            ViewBag.Hidden_JobCodes = Hidden_JobCodes;
            var schools = (from f in db.ReportDatas
                           select new { School = f.School }).Distinct();

            ViewBag.Schools = new SelectList(schools.OrderBy(x => x.School), "School", "School");
            ViewBag.Hidden_Schools = Hidden_Schools;
            var years = (from f in db.ReportDatas
                         select new { Year = f.Year }).Distinct();

            ViewBag.Years = new SelectList(years.OrderBy(x => x.Year), "Year", "Year");
            ViewBag.Hidden_Years = Hidden_Years;
            return View(db.SchoolQualative(Hidden_Years.Length > 0 ? Hidden_Years : null, Hidden_Schools.Length > 0 ? Hidden_Schools : null, Hidden_JobCodes.Length > 0 ? Hidden_JobCodes : null).ToList());
        }

        

    }
}