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
using VacaYAY.Entities.Requests;
using Microsoft.AspNet.Identity;
using VacaYAY.Entities.Employees;
using VacaYAY.ViewModels;
using VacaYAY.Business.DTOs;

namespace VacaYAY.Controllers
{
    public class RequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                return View(DetailsRequestViewModel.ToVMs(RequestService.All()));
            }
            else if (User.IsInRole("Employee"))
            {
                return View(DetailsRequestViewModel.ToVMs(RequestService.AllUsersRequests(User.Identity.GetUserId())));
            }
            return View(DetailsRequestViewModel.ToVMs(RequestService.AllPending()));
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DetailsRequestDTO dto = RequestService.GetDetailsDTO(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }
            return View(DetailsRequestViewModel.ToVM(dto));
        }
        public ActionResult AsyncDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DetailsRequestDTO dto = RequestService.GetDetailsDTO(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }
            return PartialView(DetailsRequestViewModel.ToVM(dto));
        }

        // GET: Requests/Create
        [Authorize]
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                int remainingDays = EmployeeService.GetUsersVacationDays(HttpContext.User.Identity.GetUserId());
                CreateRequestViewModel model = new CreateRequestViewModel()
                {
                    RemainingVacationDays = remainingDays,
                };
                return PartialView(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                RequestService requestService = new RequestService();
                string userID = HttpContext.User.Identity.GetUserId();
                if (RequestService.Add(CreateRequestViewModel.ToDTO(request), userID))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            EditRequestDTO dto = RequestService.GetEditDTO(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }
            return View(EditRequestViewModel.ToViewModel(dto));
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                RequestService.Update(EditRequestViewModel.ToDTO(request));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AsyncEdit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            EditRequestDTO dto = RequestService.GetEditDTO(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }
            return PartialView(EditRequestViewModel.ToViewModel(dto));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsyncEdit(EditRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                RequestService.Update(EditRequestViewModel.ToDTO(request));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult AsyncApprove(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DetailsRequestDTO dto = RequestService.GetDetailsDTO(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }
            return PartialView(DetailsRequestViewModel.ToVM(dto));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsyncApprove(int id)
        {
            if (ModelState.IsValid)
            {
                int HR_ID = (EmployeeService.GetEmployeeIDWithUserID(HttpContext.User.Identity.GetUserId()));
                RequestService.Approve(id, HR_ID);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult AsyncReject(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DetailsRequestDTO dto = RequestService.GetDetailsDTO(id);
            if (dto == null)
            {
                return RedirectToAction("Index");
            }
            return PartialView(DetailsRequestViewModel.ToVM(dto));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsyncReject(int id)
        {
            if (ModelState.IsValid)
            {
                int HR_ID = (EmployeeService.GetEmployeeIDWithUserID(HttpContext.User.Identity.GetUserId()));
                RequestService.Reject(id, HR_ID);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
