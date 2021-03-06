﻿        using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VacaYAY.Business;
using VacaYAY.Data;
using VacaYAY.Entities.Contracts;
using VacaYAY.ViewModels;

namespace VacaYAY.Controllers
{
    public class ContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contracts
        public ActionResult Index()
        {
            return View(db.Contracts.ToList());
        }

        // GET: Contracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contracts/Create
        public ActionResult Create(int id)
        {
            CreateContractViewModel vm = new CreateContractViewModel()
            {
                EmployeeID = id,
            };
            return PartialView(vm);
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractID,Text,StartDate,EndDate,Link")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contract);
        }

        // GET: Contracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractID,Text,StartDate,EndDate,Link")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contract);
        }
        public ActionResult AsyncCreate(int id)
        {
            if (id==0)
            {
                return RedirectToAction("Index", "Requests");
            }
            CreateContractViewModel vm = new CreateContractViewModel()
            {
                EmployeeID = id,
            };
            return PartialView(vm);
        }
        [HttpGet]
        public ActionResult RemoveContract(int? id)
        {
            EditEmployeeContractViewModel vm = EditEmployeeContractViewModel.ToVM(ContractService.GetEditContract(id));
            vm.Link = HttpContext.Server.MapPath(vm.Link);
            return PartialView(vm);
        }
        [HttpPost]
        public ActionResult RemoveContract(EditEmployeeContractViewModel vm)
        {

            if (vm != null)
            {
                string returned = EmployeeService.RemoveContract(vm.ContractID);
                if (returned == "OK")
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return RedirectToAction("Index", "Employees");
            }
            EditEmployeeContractViewModel vm = EditEmployeeContractViewModel.ToVM(ContractService.GetEditContract(id));
            vm.Link = HttpContext.Server.MapPath(vm.Link);
            return PartialView(vm);
        }

        // POST: Contracts/Delete/5
        [HttpPost]
        public JsonResult Delete(EditEmployeeContractViewModel vm)
        {
            if (vm != null)
            {
                string returned = EmployeeService.RemoveContract(vm.ContractID);
                return Json(returned);
            }
            return Json("Invalid model error");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
