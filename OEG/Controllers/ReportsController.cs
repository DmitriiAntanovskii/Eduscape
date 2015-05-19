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


    }
}