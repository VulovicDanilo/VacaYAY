using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.Owin;
using System.Web.Security;
using VacaYAY.Business.DTOs;
using VacaYAY.Data;
using VacaYAY.Data.Repos;
using VacaYAY.Entities;
using VacaYAY.Entities.Contracts;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;
using static VacaYAY.Common.Enums;
using Microsoft.Owin.Security;
using VacaYAY.Entities.ExtraDays;
using System.IO;

namespace VacaYAY.Business
{
    public class EmployeeService
    {
        public static EmployeeRepository repo = new EmployeeRepository();
        public static ContractRepository contractRepo = new ContractRepository();
        public static ExtraDaysRepository extraRepo = new ExtraDaysRepository();

        public static Employee GetEmployeeWithUserID(string id)
        {
            return repo.GetEmployeeByUserID(id);
        }
        public static int GetEmployeeIDWithUserID(string id)
        {
            return repo.GetEmployeeIDByUserID(id);
        }
        public static List<Employee> GetAllActiveEmployees()
        {
            return repo.AllActiveEmployees();
        }
        public static List<Employee> GetAllInactiveEmployees()
        {
            return repo.AllInactiveEmployees();
        }
        public static List<Resolution> GetUsersResolutions(int? id)
        {
            return repo.GetUsersResolutions(id);
        }
        public static List<string> GetManagerEmails()
        {
            return repo.GetManagerEmails();
        }
        public static List<string> GetActiveEmployeeEmails()
        {
            List<Employee> employees = GetAllActiveEmployees();
            return employees.Select(x => x.User.Email).ToList();
        }
        public static int GetUsersVacationDays(string id)
        {
            return repo.GetUsersVacationDays(id);
        }
        public static string GetUserEmail(string id)
        {
            return repo.GetUserEmail(id);
        }
        public static Employee GetEmployee(int? id)
        {
            return repo.Find(id);
        }
        public static bool Update(Employee employee)
        {
            return repo.Update(employee);
        }
        public static bool SoftDelete(int? id)
        {
            return repo.SoftDelete(id);
        }
        public static bool Restore(int? id)
        {
            return repo.Restore(id);
        }
        public static List<IndexEmployeeDTO> GetIndexEmployees()
        {
            List<Employee> employees = repo.All();
            return IndexEmployeeDTO.ToDTOs(employees);
        }
        public static List<IndexEmployeeDTO> GetIndexActiveEmployees()
        {
            List<Employee> employees = repo.AllActiveEmployees();
            return IndexEmployeeDTO.ToDTOs(employees);
        }
        public static List<IndexEmployeeDTO> GetIndexInactiveEmployees()
        {
            List<Employee> employees = repo.AllInactiveEmployees();
            return IndexEmployeeDTO.ToDTOs(employees);
        }
        public static bool RegisterEmployee(RegisterEmployeeDTO dto)
        {
            Employee emp = RegisterEmployeeDTO.ToEntity(dto);
            return repo.Add(emp);
        }
        public static EditEmployeeDTO GetEmployeeEdit(int? id)
        {
            EditEmployeeDTO dto = EditEmployeeDTO.ToDTO(repo.Find(id));
            var resolutions = ResolutionService.GetEmployeesResolutions(id);
            dto.AddResolutions(resolutions);
            return dto;
        }
        public static DetailsEmployeeDTO GetEmployeeDetails(int? id)
        {
            DetailsEmployeeDTO dto = DetailsEmployeeDTO.ToDTO(repo.Find(id));
            var resolutions = ResolutionService.GetEmployeesResolutions(id);
            dto.AddResolutions(resolutions);
            var extraDays = ExtraDaysService.GetEmployeesExtraDays(id);
            dto.AddExtraDays(extraDays);
            return dto;
        }
        public static string GetUserIDWithEmployeeID(int? id)
        {
            return repo.GetUserIDWithEmployeeID(id);
        }
        public static EditVacationDaysDTO GetVacDays(int? id)
        {
            return EditVacationDaysDTO.ToDTO(repo.Find(id));
        }
        public static string SetVacDays(EditVacationDaysDTO dto)
        {
            Employee employee = repo.Find(dto.EmployeeID);
            employee.CurrentVacationDays = dto.CurrentVacationDays;
            employee.ExtraVacationDays = dto.ExtraVacationDays;
            employee.UsedPaidVacationDays = dto.UsedPaidVacationDays;
            employee.LeftoverVacationDays = dto.LeftoverVacationDays;
            if(repo.Update(employee))
            {
                return "OK";
            }
            else
            {
                return "Error editing vacation days [Database error]";
            }
        }
        public static bool UpdateEmployee(EditEmployeeDTO dto)
        {
            string userID = GetUserIDWithEmployeeID(dto.EmployeeID);

            Employee emp = repo.Find(dto.EmployeeID);
            emp.Name = dto.Name;
            emp.LastName = dto.LastName;
            emp.City = dto.City;
            emp.Profession = dto.Profession;
            if (emp.IsManager != dto.IsManager)
            {
                if (EditRole(userID, dto.IsManager))
                    emp.IsManager = dto.IsManager;
            }
            return repo.Update(emp);
        }
        public static string AddContract(CreateContractDTO dto)
        {
            // TODO Onemoguciti preklapanje
            string msg = CreateContractValidation(dto);
            if (msg != "OK")
                return msg;
            Employee employee = EmployeeService.GetEmployee(dto.EmployeeID);
            string userID = EmployeeService.GetUserIDWithEmployeeID(employee.EmployeeID);
            Contract contract = CreateContractDTO.ToEntity(dto);
            FileInfo fileInfo = new FileInfo(dto.File.FileName);
            var fileName = DateTime.Now.ToString("yyyyMMdd_hhmmss") + fileInfo.Extension;
            var filepath = "~/Contracts/" + userID + "/";
            var directory = HttpContext.Current.Server.MapPath(filepath);
            var path = System.IO.Path.Combine(directory, fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            else if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            dto.File.SaveAs(path);
            contract.Link = filepath + fileName;
            employee.Contracts.Add(contract);
            AddVacationDays(employee);
            if (repo.Update(employee))
            {
                return "OK";
            }
            else
            {
                return "Database error[Create Contract]";
            }
        }
        public static string RemoveContract(int? ContractID)
        {
            Contract contract = contractRepo.Find(ContractID);
            Employee employee = repo.Find(contract.EmployeeID);
            var path = HttpContext.Current.Server.MapPath(contract.Link);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            RemoveVacationDays(contract);
            employee.Contracts.Remove(employee.Contracts.Where(x => x.ContractID == contract.ContractID).First());
            repo.Update(employee);
            if(contractRepo.Delete(contract.ContractID))
            {
                return "OK";
            }
            else
            {
                return "Unable to remove contract [Database error]";
            }
        }
        public static string AddExtraDays(CreateExtraDaysDTO dto)
        {
            Employee employee = repo.Find(dto.EmployeeID);
            employee.ExtraVacationDays += dto.Days;
            employee.CurrentVacationDays += dto.Days;
            employee.ExtraDays.Add(CreateExtraDaysDTO.ToEntity(dto));
            if (repo.Update(employee))
            {
                return "OK";
            }
            else
            {
                return "Database error";
            }
        }
        public static bool RemoveExtraDays(int ExtraDaysID)
        {
            ExtraDays extraDays = extraRepo.Find(ExtraDaysID);
            Employee employee = repo.Find(extraDays.EmployeeID);
            if (employee.CurrentVacationDays < extraDays.Days)
                employee.CurrentVacationDays = 0;
            else
                employee.CurrentVacationDays -= extraDays.Days;
            if (employee.ExtraVacationDays < extraDays.Days)
                employee.ExtraVacationDays = 0;
            else
                employee.ExtraVacationDays -= extraDays.Days;
            employee.ExtraDays.Remove(employee.ExtraDays.Where(x => x.ExtraDaysID == extraDays.ExtraDaysID).First());
            repo.Update(employee);
            return extraRepo.Delete(extraDays.ExtraDaysID);
        }
        public static bool IsActive(string email)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(store);
            var u = _userManager.FindByEmail(email);
            if (u == null)
                return false;
            var employeeID = EmployeeService.GetEmployeeIDWithUserID(u.Id);
            Employee employee = EmployeeService.GetEmployee(employeeID);
            return employee.Active;
        }
        #region helpers
        private static int CalculateVacationDaysForDates(DateTime start, DateTime end)
        {
            int numOfMonths = 0;
            double days = 0;
            numOfMonths = ((end.Year - start.Year) * 12) + end.Month - start.Month;
            days = numOfMonths * (((double)20 / 12));
            return (int)days;
        }
        private static void AddVacationDays(Employee employee)
        {
            int days = 0;
            DateTime start = employee.Contracts.Last().StartDate;
            DateTime? end = employee.Contracts.Last().EndDate;
            if (end == null || end.Value.Year > start.Year)
            {
                // Ako je neodredjeno ili ako ugovor ide u narednu godinu
                end = new DateTime(start.Year, 12, 31);
            }
            days = CalculateVacationDaysForDates(start, end.Value);
            employee.CurrentVacationDays += days;
        }
        private static void RemoveVacationDays(Contract contract)
        {
            int days = 0;
            DateTime start = contract.StartDate;
            DateTime? end = contract.EndDate;
            if (start.Year < DateTime.Now.Year)
            {
                start = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (end == null || end.Value.Year > DateTime.Now.Year)
            {
                end = new DateTime(DateTime.Now.Year, 12, 31);
            }
            days = CalculateVacationDaysForDates(start, end.Value);
            Employee employee = repo.Find(contract.EmployeeID);
            employee.CurrentVacationDays -= days;
            if (employee.CurrentVacationDays < 0)
                employee.CurrentVacationDays = 0;
        }

        private static bool EditRole(string userID, bool IsManager)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(store);
            var roles = _userManager.GetRoles(userID);
            if (_userManager.RemoveFromRole(userID, roles[0]).Succeeded)
            {
                if (IsManager)
                {
                    _userManager.AddToRole(userID, "Manager");
                }
                else
                {
                    _userManager.AddToRole(userID, "Employee");
                }
                return true;
            }
            return false;
        }
        private static string CreateContractValidation(CreateContractDTO dto)
        {
            if (dto.StartDate<DateTime.Now.AddDays(1))
            {
                return "Start Date must be at least tommrow";
            }
            if (dto.EndDate!=null)
            {
                if (dto.StartDate>dto.EndDate)
                {
                    return "Start Date must be earlier than End Date";
                }
                else if (dto.StartDate.AddMonths(3)>dto.EndDate)
                {
                    return "Contract must be for at least 3 months";
                }
            }
            return "OK";
        }
        #endregion

    }
}