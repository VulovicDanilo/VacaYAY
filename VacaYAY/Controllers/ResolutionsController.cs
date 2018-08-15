using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VacaYAY.Data;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Controllers
{
    public class ResolutionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Resolutions
        public ActionResult Index()
        {
            return View(db.Resolutions.ToList());
        }

        // GET: Resolutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resolution resolution = db.Resolutions.Find(id);
            if (resolution == null)
            {
                return HttpNotFound();
            }
            return View(resolution);
        }

        // GET: Resolutions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resolutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResolutionID,ApprovalDate,SerialNumber")] Resolution resolution)
        {
            if (ModelState.IsValid)
            {
                db.Resolutions.Add(resolution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resolution);
        }

        // GET: Resolutions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resolution resolution = db.Resolutions.Find(id);
            if (resolution == null)
            {
                return HttpNotFound();
            }
            return View(resolution);
        }

        // POST: Resolutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResolutionID,ApprovalDate,SerialNumber")] Resolution resolution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resolution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resolution);
        }

        // GET: Resolutions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resolution resolution = db.Resolutions.Find(id);
            if (resolution == null)
            {
                return HttpNotFound();
            }
            return View(resolution);
        }

        // POST: Resolutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resolution resolution = db.Resolutions.Find(id);
            db.Resolutions.Remove(resolution);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
