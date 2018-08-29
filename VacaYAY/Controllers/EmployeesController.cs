using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using VacaYAY.Business;
using VacaYAY.Business.DTOs;
using VacaYAY.Data;
using VacaYAY.Entities.Employees;
using VacaYAY.ViewModels;

namespace VacaYAY.Controllers
{
    [Authorize(Roles = "Manager,Employee")]
    public class EmployeesController : Controller
    {

        // GET: Employees
        public ActionResult Index()
        {
            var employees = EmployeeService.GetIndexEmployees();
            return View(IndexEmployeeViewModel.ToVMs(employees).AsEnumerable());
        }
        public ActionResult UserProfile()
        {
            //string userID = User.Identity.GetUserId();
            string userID = User.Identity.GetUserId();
            DetailsEmployeeDTO dto = EmployeeService.GetEmployeeDetails(EmployeeService.GetEmployeeIDWithUserID(userID));
            DetailsEmployeeViewModel vm = DetailsEmployeeViewModel.ToVM(dto);
            foreach (var contract in vm.Contracts)
            {
                contract.Link = HttpContext.Server.MapPath(contract.Link);
            }
            foreach (var resolution in vm.Resolutions)
            {
                resolution.Link = HttpContext.Server.MapPath(resolution.Link);
            }
            return View(vm);

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
                return RedirectToAction("Index");
            }
            DetailsEmployeeViewModel vm = DetailsEmployeeViewModel.ToVM(EmployeeService.GetEmployeeDetails(id));
            if (vm == null)
            {
                return HttpNotFound();
            }
            foreach (var contract in vm.Contracts)
            {
                contract.Link = HttpContext.Server.MapPath(contract.Link);
            }
            foreach (var resolution in vm.Resolutions)
            {
                resolution.Link = HttpContext.Server.MapPath(resolution.Link);
            }
            return View(vm);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterEmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeService.RegisterEmployee(RegisterEmployeeViewModel.ToDTO(employee));
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("UserProfile");
            }
            EditEmployeeDTO employeeDTO = EmployeeService.GetEmployeeEdit(id);
            if (employeeDTO == null)
            {
                return HttpNotFound();
            }
            EditEmployeeViewModel vm = EditEmployeeViewModel.ToVM(employeeDTO);
            if (EmployeeService.GetEmployeeIDWithUserID(User.Identity.GetUserId()) == vm.EmployeeID)
            {
                vm.IsHimself = true;
            }
            HttpContext.Session["OldContracts"] = vm.Contracts;
            foreach (var contract in vm.Contracts)
            {
                contract.Link = HttpContext.Server.MapPath(contract.Link);
            }
            return View(vm);
        }
        public FileResult Download(string filename = "")
        {
            FileInfo file = new FileInfo(filename);
            var fileContents = System.IO.File.ReadAllBytes(filename);
            var response = new FileContentResult(fileContents, "application/octet-stream")
            {
                FileDownloadName = file.Name
            };
            return response;
        }
        public ActionResult Preview(string filename)
        {
            FileInfo file = new FileInfo(filename);
            var fileContents = System.IO.File.ReadAllBytes(filename);
            if (fileContents == null)
            {
                return null;
            }
            var contentDispositionHeader = new ContentDisposition()
            {
                Inline = true,
                FileName = file.Name,
            };
            Response.Headers.Add("Content-Disposition", contentDispositionHeader.ToString());
            if (file.Extension == ".pdf")
                return File(fileContents, MediaTypeNames.Application.Pdf);
            else
                return File(fileContents, MediaTypeNames.Application.Octet);
        }
        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditEmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                EditEmployeeDTO dto = EditEmployeeViewModel.ToDTO(vm);

                List<EditEmployeeContractViewModel> OldContracts = HttpContext.Session["OldContracts"] as List<EditEmployeeContractViewModel>;


                if (EmployeeService.UpdateEmployee(dto))
                {
                    return RedirectToAction("Details", "Employees", new { id = vm.EmployeeID });
                }
                else
                {
                    return RedirectToAction("Edit", "Employees", new { id = vm.EmployeeID });
                }
            }
            return RedirectToAction("Details", "Employees", new { id = vm.EmployeeID });
        }
        public ActionResult EditVacDays(int? id)
        {
            if (id != null)
            {
                EditVacationDaysViewModel vm = EditVacationDaysViewModel.ToVM(EmployeeService.GetVacDays(id));
                return PartialView(vm);
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public JsonResult EditVacDays(EditVacationDaysViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string returned = EmployeeService.SetVacDays(EditVacationDaysViewModel.FromVM(vm));
                return Json(returned);
            }
            return Json("Invalid model error");
        }

        public JsonResult AddContract(CreateContractViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CreateContractDTO dto = CreateContractViewModel.ToDTO(vm);
                string returned = EmployeeService.AddContract(dto);
                return Json(returned);
            }
            return Json("Model state invalid");
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
        public ActionResult Restore(int? id)
        {
            if (EmployeeService.Restore(id))
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
