using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VacaYAY.Business;
using VacaYAY.Data;
using VacaYAY.Entities.ExtraDays;
using VacaYAY.ViewModels;

namespace VacaYAY.Controllers
{
    public class ExtraDaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExtraDays
        public ActionResult Index()
        {
            var extraDays = db.ExtraDays.Include(e => e.Employee);
            return View(extraDays.ToList());
        }
        public ActionResult CreateAsync(int id)
        {
            CreateExtraDaysViewModel vm = new CreateExtraDaysViewModel()
            {
                EmployeeID = id,
            };
            return PartialView(vm);
        }
        [HttpPost]
        public JsonResult CreateAsync(CreateExtraDaysViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.TimeStamp = DateTime.Now;
                string returned = EmployeeService.AddExtraDays(CreateExtraDaysViewModel.ToDTO(vm));
                return Json(returned);
            }
            return Json("Model state error");
        }
        [HttpGet]
        public ActionResult RemoveExtraDays(int? id)
        {
            ExtraDaysViewModel vm = ExtraDaysViewModel.ToVM(ExtraDaysService.Find(id));
            return PartialView(vm);
        }
        [HttpPost]
        public ActionResult RemoveExtraDays(ExtraDaysViewModel vm)
        {
            if (vm != null)
            {
                if (EmployeeService.RemoveExtraDays(vm.ExtraDaysID))
                {
                    return RedirectToAction("Edit", "Employees", new { id = vm.EmployeeID });
                }
                else
                {
                    ModelState.AddModelError("", "Couldnt remove extra days");
                    return RedirectToAction("Details", "Employees", new { id = vm.EmployeeID });
                }
            }
            return RedirectToAction("Index");
        }
        // GET: ExtraDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraDays extraDays = db.ExtraDays.Find(id);
            if (extraDays == null)
            {
                return HttpNotFound();
            }
            return View(extraDays);
        }

        // GET: ExtraDays/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name");
            return View();
        }

        // POST: ExtraDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExtraDaysID,Timestamp,Days,Basis,EmployeeID")] ExtraDays extraDays)
        {
            if (ModelState.IsValid)
            {
                db.ExtraDays.Add(extraDays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", extraDays.EmployeeID);
            return View(extraDays);
        }

        // GET: ExtraDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraDays extraDays = db.ExtraDays.Find(id);
            if (extraDays == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", extraDays.EmployeeID);
            return View(extraDays);
        }

        // POST: ExtraDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExtraDaysID,Timestamp,Days,Basis,EmployeeID")] ExtraDays extraDays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraDays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", extraDays.EmployeeID);
            return View(extraDays);
        }

        // GET: ExtraDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraDays extraDays = db.ExtraDays.Find(id);
            if (extraDays == null)
            {
                return HttpNotFound();
            }
            return View(extraDays);
        }

        // POST: ExtraDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraDays extraDays = db.ExtraDays.Find(id);
            db.ExtraDays.Remove(extraDays);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
