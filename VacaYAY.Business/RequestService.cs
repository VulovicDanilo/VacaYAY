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
        private static EmployeeRepository empRepo = new EmployeeRepository();

        public static string Add(CreateRequestDTO dto, string userID)
        {
            string msg = CreateRequestValidation(dto, userID);
            if (msg != "OK")
                return msg;
            Request request = CreateRequestDTO.ToEntity(dto);
            Employee employee = EmployeeService.GetEmployeeWithUserID(userID);
            request.EmployeeID = employee.EmployeeID;
            int days = CalculateNumberOfWorkingDays(request.StartDate, request.EndDate);
            request.NumberOfDays = days;
            if (request.Comments.Count > 0)
            {
                request.Comments.First().CommenterID = employee.EmployeeID;
            }
            if (repo.Add(request))
            {
                EmailSender es = new EmailSender();
                EmailSender.SendNewRequestEmailToAllManagers(request);
                return "OK";
            }
            return "Failed to add request [Database error]";

        }



        public static List<DetailsRequestDTO> AllUsersRequests(string userID)
        {
            List<DetailsRequestDTO> dtos = DetailsRequestDTO.ToDTOs(repo.AllUsersRequests(userID));
            return dtos;
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
            List<DetailsRequestDTO> list = DetailsRequestDTO.ToDTOs(repo.All());

            foreach (var item in list)
            {
                if (item.RequestID == id)
                    return item;
                else
                {
                    foreach (var emp in item.collectiveEmployees)
                    {
                        if (emp.RequestID == id)
                            return item;
                    }
                }
            }
            return null;
        }
        public static string Update(EditRequestDTO dto)
        {
            string msg = EditRequestValidation(dto);
            if (msg != "OK")
                return msg;
            Request request = (EditRequestDTO.ToEntity(dto));
            if (repo.Update(request))
            {
                EmailSender es = new EmailSender();
                EmailSender.SendEditRequestEmailToAllManagers(request);
                return "OK";
            }
            return "Failed to edit request [Database error]";
        }

        public static bool IsCreatorManager(int RequestID)
        {
            Request request = repo.Find(RequestID);
            if (request != null)
            {
                return request.Employee.IsManager;
            }
            else
            {
                return false;
            }
        }
        public static List<DetailsRequestDTO> All()
        {
            return DetailsRequestDTO.ToDTOs(repo.All());
        }
        public static List<DetailsRequestDTO> AllPending()
        {
            return DetailsRequestDTO.ToDTOs(repo.AllPending());
        }
        public static List<Request> AllApproved()
        {
            return repo.AllApproved();
        }
        public static List<Request> AllRejected()
        {
            return repo.AllRejected();
        }
        public static string Approve(int RequestID, int HRID,string basis)
        {
            Request request = RequestService.GetRequest(RequestID);
            Employee employee = EmployeeService.GetEmployee(request.EmployeeID);
            Employee HR = EmployeeService.GetEmployee(HRID);
            Resolution resolution = new Resolution()
            {
                RequestID = request.RequestID,
                HR_ID = HR.EmployeeID,
                ApprovalDate = DateTime.Now,
            };
            if (request.TypeOfDays == TypeOfDays.Regular)
            {
                string msg = ApproveRegularRequestValidation(RequestID);
                if (msg != "OK")
                    return msg;
                if (!ResolutionService.Add(resolution))
                    return "Failed to add resolution [Database error]";
                resolution.SerialNumber = resolution.ResolutionID.ToString() + "/" + DateTime.Now.Year;
                // TODO PDF GENERATE REGULAR VACATION
                //request.Employee.CurrentVacationDays -= request.NumberOfDays;
                if (request.NumberOfDays<=employee.LeftoverVacationDays)
                {
                    employee.LeftoverVacationDays -= request.NumberOfDays;
                    resolution.LeftoverUsed = request.NumberOfDays;
                    resolution.RegularUsed = 0;
                }
                else
                {
                    int diff = request.NumberOfDays-employee.LeftoverVacationDays;

                    resolution.RegularUsed = diff;
                    resolution.LeftoverUsed = employee.LeftoverVacationDays;

                    employee.LeftoverVacationDays = 0;
                    employee.CurrentVacationDays -= diff;
                }
                if (!EmployeeService.Update(employee))
                    return "Failed to update employee [Database error] => Employee:" + employee.Name + " " + employee.LastName;
                request.Status = Status.Approved;
                request.Comments.Last().Status = request.Status;
                PDFGen generator = new PDFGen();
                string filepath = "~/Resolutions/" + request.Employee.UserID + "/";
                var directory = HttpContext.Current.Server.MapPath(filepath);
                string filename = generator.GenerateResolution(request);
                FileInfo file = new FileInfo(filename);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                file.MoveTo(directory + filename);
                resolution.Link = filepath + filename;
                EmailSender.ApproveRegularVacation(request, HR, file.FullName);
                if (!ResolutionService.Update(resolution))
                    return "Failed to update resolution [Database error]";
                return repo.Update(request) ? "OK" : "Cant approve regular request [Database error]";
            }
            else if (request.TypeOfDays == TypeOfDays.Paid)
            {
                string msg = ApprovePaidRequestValidation(RequestID,basis);
                if (msg != "OK")
                    return msg;
                // ne skidaj dane slobodnog odmora
                // TODO PDF GENERATE PAID AND UNPAID VACATION
                resolution.Basis = basis;
                if (!ResolutionService.Add(resolution))
                    return "Failed to add resolution [Database error]";
                resolution.SerialNumber = resolution.ResolutionID.ToString() + "/" + DateTime.Now.Year;
                int days = CalculateNumberOfWorkingDays(request.StartDate, request.EndDate);
                employee.UsedPaidVacationDays += days;
                if (!EmployeeService.Update(employee))
                    return "Failed to update employee [Database error] => Employee:" + employee.Name + " " + employee.LastName;
                request.Status = Status.Approved;
                if (request.Comments.Count > 0)
                    request.Comments.Last().Status = request.Status;
                PaidReportDTO report = new PaidReportDTO(employee, request, resolution);
                PDFGen generator = new PDFGen();
                string filepath = "~/Resolutions/" + request.Employee.UserID + "/";
                var directory = HttpContext.Current.Server.MapPath(filepath);
                string filename = generator.GenerateResolution(request);
                FileInfo file = new FileInfo(filename);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                file.MoveTo(directory + filename);
                resolution.Link = filepath + filename;
                EmailSender.ApprovePaidVacation(request, HR, file.FullName);
                if (!ResolutionService.Update(resolution))
                    return "Failed to update resolution [Database error]";
                return repo.Update(request) ? "OK" : "Cant approve paid request [Database error]";
            }
            else if (request.TypeOfDays == TypeOfDays.Unpaid)
            {
                request.Status = Status.Approved;
                if (request.Comments.Count > 0)
                    request.Comments.Last().Status = Status.Approved;
                EmailSender.ApproveUnpaidVacation(request, HR);
                return repo.Update(request) ? "OK" : "Cant approve unpaid request [Database error]";

            }
            return "TypeOfDays not specified";
        }
        public static bool Reject(int RequestID, int HRID)
        {
            Request request = RequestService.GetRequest(RequestID);
            Employee HR = EmployeeService.GetEmployee(HRID);
            request.Status = Status.Rejected;
            if (request.Comments.Count > 0)
            {
                request.Comments.Last().Status = request.Status;
            }
            EmailSender.RejectRequest(request, HR);
            return repo.Update(request);
        }
        public static string AddCollective(CreateCollectiveDTO dto, string HRID)
        {
            string msg = CreateCollectiveVacationValidation(dto);
            if (msg != "OK")
                return msg;

            List<Employee> employees = EmployeeService.GetAllActiveEmployees();
            Employee HR = EmployeeService.GetEmployeeWithUserID(HRID);
            DateTime subDate = DateTime.Now;
            int days = CalculateNumberOfWorkingDays(dto.StartDate, dto.EndDate);

            foreach (var employee in employees)
            {
                Request request = CreateCollectiveDTO.ToEntity(dto);
                request.TypeOfDays = TypeOfDays.Collective;
                request.Status = Status.Approved;
                request.EmployeeID = employee.EmployeeID;
                request.NumberOfDays = CalculateNumberOfWorkingDays(request.StartDate, request.EndDate);
                request.SubmissionDate = subDate;

                // TODO Lower everyones Vacation Days!!!
                if (employee.CurrentVacationDays >= days)
                {
                    employee.CurrentVacationDays -= days;
                    if (!EmployeeService.Update(employee))
                    {
                        return "Failed to update employee [Database error] => Employee:" + employee.Name + " " + employee.LastName;
                    }
                    RequestRepository requestRepository = new RequestRepository();
                    if (requestRepository.Add(request))
                    {
                        Resolution resolution = new Resolution()
                        {
                            RequestID = request.RequestID,
                            ApprovalDate = subDate,
                            HR_ID = HR.EmployeeID,
                        };
                        if (!ResolutionService.Add(resolution))
                        {
                            return "Failed to add resolution [Database error]";
                        }
                        resolution.SerialNumber = resolution.ResolutionID.ToString() + "/" + DateTime.Now.Year;
                        PDFGen generator = new PDFGen();
                        string filepath = "~/Resolutions/" + request.Employee.UserID + "/";
                        var directory = HttpContext.Current.Server.MapPath(filepath);
                        string filename = generator.GenerateResolution(request);
                        FileInfo file = new FileInfo(filename);
                        if (!Directory.Exists(directory))
                            Directory.CreateDirectory(directory);
                        file.MoveTo(directory + filename);
                        resolution.Link = filepath + filename;
                        EmailSender.SendCollective(request, HR, file.FullName);
                        if (!ResolutionService.Update(resolution))
                        {
                            return "Failed to update resolution [Database error]";
                        }
                    }
                }
                else
                {
                    return "Failed to add request [Database error]";
                }
            }
            return "OK";
        }
        private static string CreateRequestValidation(CreateRequestDTO dto, string userID)
        {
            Employee employee = EmployeeService.GetEmployeeWithUserID(userID);
            if (dto.StartDate > dto.EndDate)
            {
                return "Start Date must be before End Date";
            }
            int days = CalculateNumberOfWorkingDays(dto.StartDate, dto.EndDate);
            if (employee.CurrentVacationDays < days)
            {
                return "You don't have enough vacation days";
            }
            if (DateTime.Now.AddDays(1) >= dto.StartDate)
            {
                return "Start Date must be later than tommorow";
            }
            return "OK";
        }
        private static string CreateCollectiveVacationValidation(CreateCollectiveDTO dto)
        {
            if (DateTime.Now.AddDays(1) >= dto.StartDate)
            {
                return "Start Date must be later than tommorow";
            }
            if (dto.StartDate > dto.EndDate)
            {
                return "Start Date must be before End Date";
            }
            return "OK";
        }
        private static string EditRequestValidation(EditRequestDTO dto)
        {
            Employee employee = EmployeeService.GetEmployee(dto.EmployeeID);
            if (employee == null)
                return "Database error";
            if (dto.StartDate > dto.EndDate)
            {
                return "Start Date must be before End Date";
            }
            if (DateTime.Now.AddDays(1) >= dto.StartDate)
            {
                return "Start Date must be later than tommorow";
            }
            int days = CalculateNumberOfWorkingDays(dto.StartDate, dto.EndDate);
            if (employee.CurrentVacationDays < days)
            {
                return "You don't have enough vacation days";
            }
            return "OK";
        }
        private static string ApproveRegularRequestValidation(int RequestID)
        {
            Request request = repo.Find(RequestID);
            if (request == null)
            {
                return "Database error [request]";
            }
            Employee employee = empRepo.Find(request.EmployeeID);
            if (employee == null)
            {
                return "Database error [employee]";
            }
            if (request.StartDate > request.EndDate)
            {
                return "Start Date must be before End Date";
            }
            if (DateTime.Now.AddDays(1) >= request.StartDate)
            {
                return "Start Date must be later than tommorow";
            }
            int days = CalculateNumberOfWorkingDays(request.StartDate, request.EndDate);
            if (employee.CurrentVacationDays+employee.LeftoverVacationDays < days)
            {
                return "You don't have enough vacation days!";
            }
            return "OK";
        }
        private static string ApprovePaidRequestValidation(int RequestID,string basis)
        {
            Request request = repo.Find(RequestID);
            if (request == null)
            {
                return "Database error [request]";
            }
            if (string.IsNullOrEmpty(basis))
            {
                return "Basis must me specified!";
            }
            Employee employee = empRepo.Find(request.EmployeeID);
            if (employee == null)
            {
                return "Database error [employee]";
            }
            if (request.StartDate > request.EndDate)
            {
                return "Start Date must be before End Date";
            }
            if (DateTime.Now.AddDays(1) >= request.StartDate)
            {
                return "Start Date must be later than tommorow";
            }
            int days = CalculateNumberOfWorkingDays(request.StartDate, request.EndDate);
            if (employee.UsedPaidVacationDays + days > 10)
            {
                return "Paid vacation days limit hit, you have " + (10 - employee.UsedPaidVacationDays) + " left";
            }
            return "OK";
        }
    }
}