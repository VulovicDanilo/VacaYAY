using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;
using VacaYAY.Data.Repos;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using Microsoft.AspNet.Identity;
using static VacaYAY.Common.Enums;
using VacaYAY.Entities.Resolutions;
using System.IO;

namespace VacaYAY.Business
{
    public class RequestService
    {
        private static RequestRepository repo = new RequestRepository();
        private static ResolutionRepository resRepo = new ResolutionRepository();

        public static bool Add(CreateRequestDTO dto,string userID)
        {
            Request request = CreateRequestDTO.ToEntity(dto);
            Employee employee = EmployeeService.GetEmployeeWithUserID(userID);
            request.EmployeeID = employee.EmployeeID;
            int days = CalculateNumberOfWorkingDays(request.StartDate, request.EndDate);
            if (employee.CurrentVacationDays >= days)
            {
                request.NumberOfDays = days;
                request.Comments.First().CommenterID = employee.EmployeeID;
                if (repo.Add(request))
                {
                    EmailSender es = new EmailSender();
                    EmailSender.SendNewRequestEmailToAllManagers(request);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        public static int CalculateNumberOfWorkingDays(DateTime start, DateTime end)
        {
            int days = 0;
            DateTime date = start;
            while (date <= end)
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    days++;
                }
                date = date.AddDays(1);
            }
            return days;
        }
        public static Request GetRequest(int? id)
        {
            return repo.Find(id);
        }
        public static EditRequestDTO GetEditDTO(int? id)
        {
            Request request = repo.Find(id);
            return EditRequestDTO.ToDTO(request);
        }
        public static DetailsRequestDTO GetDetailsDTO(int? id)
        {
            Request request = repo.Find(id);
            return DetailsRequestDTO.ToDTO(request);
        }
        public static bool Update(EditRequestDTO dto)
        {
            Request request = (EditRequestDTO.ToEntity(dto));
            if (repo.Update(request))
            {
                EmailSender es = new EmailSender();
                EmailSender.SendEditRequestEmailToAllManagers(request);
                return true;
            }
            return false;
        }
        public static List<Request> All()
        {
            return repo.All();
        }
        public static List<Request> AllPending()
        {
            return repo.AllPending();
        }
        public static List<Request> AllApproved()
        {
            return repo.AllApproved();
        }
        public static List<Request> AllRejected()
        {
            return repo.AllRejected();
        }
        public static bool Approve(int RequestID, int HRID)
        {
            Request request = RequestService.GetRequest(RequestID);
            Employee HR = EmployeeService.GetEmployee(HRID);
            Resolution resolution = new Resolution()
            {
                RequestID = request.RequestID,
                HR_ID = HR.EmployeeID,
                ApprovalDate = DateTime.Now,
            };
            if (request.Employee.CurrentVacationDays >= request.NumberOfDays)
            {
                request.Employee.CurrentVacationDays -= request.NumberOfDays;
                request.Status = Status.Approved;
                // TODO Generate resolution PDF and store it and send it via e-mail and set 

                PDFGen generator = new PDFGen();
                string filepath = "~/Resolutions/" + request.Employee.UserID + "/";
                var directory = HttpContext.Current.Server.MapPath(filepath);
                string filename = generator.GenerateResolution(request);
                FileInfo file = new FileInfo(filename);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                file.MoveTo(directory+filename);
                resolution.Link = filepath + filename;
                EmailSender.ApproveRequest(request, HR,file.FullName);
                ResolutionService.Add(resolution);
                return repo.Update(request);
            }
            return false;
        }
        public static bool Reject(int RequestID, int HRID)
        {
            Request request = RequestService.GetRequest(RequestID);
            Employee HR = EmployeeService.GetEmployee(HRID);
            request.Status = Status.Rejected;
            EmailSender.RejectRequest(request,HR);
            return repo.Update(request);
        }
    }
}