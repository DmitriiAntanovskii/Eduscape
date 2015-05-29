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
            return View(db.AllCoursesBenchmark().ToList());
        }

        public ActionResult YearLevelBenchmark()
        {
            return View(db.YearLevelBenchmark().ToList());
        }

        public ActionResult SchoolQuantativeByGroup()
        {
            return View(db.SchoolQuantativeByGroup().ToList());
        }

        public ActionResult SchoolQualative()
        {
            return View(db.SchoolQualative().ToList());
        }


        

    }
}