﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VacaYAY.Business;
using VacaYAY.Data;
using VacaYAY.Entities.Employees;
using VacaYAY.ViewModels;

namespace VacaYAY.Controllers
{
    public class EmployeesController : Controller
    {

        // GET: Employees
        public ActionResult Index()
        {
            var employees = EmployeeService.GetIndexEmployees();
            return View(IndexEmployeeViewModel.ToVMs(employees).AsEnumerable());
        }
        public ActionResult Active()
        {
            var employees = EmployeeService.GetIndexActiveEmployees();
            return View(IndexEmployeeViewModel.ToVMs(employees).AsEnumerable());
        }
        public ActionResult Inactive()
        {
            var employees = EmployeeService.GetIndexInactiveEmployees();
            return View(IndexEmployeeViewModel.ToVMs(employees).AsEnumerable());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = EmployeeService.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterEmployeeViewModel employee)
        {
            List<CreateContractViewModel> contracts = new List<CreateContractViewModel>();

            string[] texts = Request.Form.GetValues("text");
            DateTime[] startDates = Request.Form.GetValues("startDate").Select(x => DateTime.Parse(x)).ToArray();
            DateTime[] endDates = Request.Form.GetValues("endDate").Select(x => DateTime.Parse(x)).ToArray();
            var files = Request.Files;
            for (int i = 0; i < texts.Length; i++)
            {
                CreateContractViewModel contract = new CreateContractViewModel();
                contract.Text = texts[i];
                contract.StartDate = startDates[i];
                contract.EndDate = endDates[i];
                if (Request.Files[i] != null)
                {
                    contract.File = Request.Files[i];
                    if (contract.EndDate > contract.StartDate)
                        contracts.Add(contract);
                }

            }
            employee.Contracts = contracts;
            if (ModelState.IsValid)
            {
                EmployeeService.RegisterEmployee(RegisterEmployeeViewModel.ToDTO(employee));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = EmployeeService.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserID = new SelectList(db.Users, "Id", "Email", employee.UserID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Name,LastName,UserID,Active,ExtraVacationDays,TotalExtraVacationDays,CurrentVacationDays,LeftoverVacationDays,isManager")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeService.Update(employee);
                return RedirectToAction("Index");
            }
            //ViewBag.UserID = new SelectList(db.Users, "Id", "Email", employee.UserID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = EmployeeService.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (EmployeeService.SoftDelete(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
