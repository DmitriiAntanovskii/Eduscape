using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OEG.Models;

namespace OEG.Controllers
{
    public class ReportDatasController : Controller
    {
        private oeg_reportsEntities db = new oeg_reportsEntities();

        // GET: ReportDatas
        public ActionResult Index()
        {
            return View(db.ReportDatas.ToList());
        }

        // GET: ReportDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportData reportData = db.ReportDatas.Find(id);
            if (reportData == null)
            {
                return HttpNotFound();
            }
            return View(reportData);
        }

        // GET: ReportDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportDataID,JobCode,Group,ID,QuestionID,PrePost,Score,QualResponse,PQA1,PQA2,PQA3,Factor,School,YearLevel,Days,Venue,Year,ProgramStartDate")] ReportData reportData)
        {
            if (ModelState.IsValid)
            {
                db.ReportDatas.Add(reportData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reportData);
        }

        // GET: ReportDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportData reportData = db.ReportDatas.Find(id);
            if (reportData == null)
            {
                return HttpNotFound();
            }
            return View(reportData);
        }

        // POST: ReportDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportDataID,JobCode,Group,ID,QuestionID,PrePost,Score,QualResponse,PQA1,PQA2,PQA3,Factor,School,YearLevel,Days,Venue,Year,ProgramStartDate")] ReportData reportData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportData);
        }

        // GET: ReportDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportData reportData = db.ReportDatas.Find(id);
            if (reportData == null)
            {
                return HttpNotFound();
            }
            return View(reportData);
        }

        // POST: ReportDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportData reportData = db.ReportDatas.Find(id);
            db.ReportDatas.Remove(reportData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
