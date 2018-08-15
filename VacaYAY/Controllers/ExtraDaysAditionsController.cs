using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VacaYAY.Data;
using VacaYAY.Entities.ExtraDaysAditions;

namespace VacaYAY.Controllers
{
    public class ExtraDaysAditionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExtraDaysAditions
        public ActionResult Index()
        {
            return View(db.ExtraDaysAditions.ToList());
        }

        // GET: ExtraDaysAditions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraDaysAdition extraDaysAdition = db.ExtraDaysAditions.Find(id);
            if (extraDaysAdition == null)
            {
                return HttpNotFound();
            }
            return View(extraDaysAdition);
        }

        // GET: ExtraDaysAditions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExtraDaysAditions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExtraDaysAditionID,Timestamp,Days,Basis")] ExtraDaysAdition extraDaysAdition)
        {
            if (ModelState.IsValid)
            {
                db.ExtraDaysAditions.Add(extraDaysAdition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(extraDaysAdition);
        }

        // GET: ExtraDaysAditions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraDaysAdition extraDaysAdition = db.ExtraDaysAditions.Find(id);
            if (extraDaysAdition == null)
            {
                return HttpNotFound();
            }
            return View(extraDaysAdition);
        }

        // POST: ExtraDaysAditions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExtraDaysAditionID,Timestamp,Days,Basis")] ExtraDaysAdition extraDaysAdition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraDaysAdition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(extraDaysAdition);
        }

        // GET: ExtraDaysAditions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraDaysAdition extraDaysAdition = db.ExtraDaysAditions.Find(id);
            if (extraDaysAdition == null)
            {
                return HttpNotFound();
            }
            return View(extraDaysAdition);
        }

        // POST: ExtraDaysAditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraDaysAdition extraDaysAdition = db.ExtraDaysAditions.Find(id);
            db.ExtraDaysAditions.Remove(extraDaysAdition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
