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
            return View(db.ProgramsBenchmark().ToList());
        }

        public ActionResult DurationBenchmark()
        {
            return View(db.DurationBenchmark().ToList());
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
            return View(db.QuantativeByGroup().ToList());
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