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

namespace VacaYAY.Business
{
    public class EmployeeService
    {
        public static EmployeeRepository repo = new EmployeeRepository();

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
        public static bool AddContract(CreateContractDTO dto)
        {
            Employee employee = EmployeeService.GetEmployee(dto.EmployeeID);
            string userID = EmployeeService.GetUserIDWithEmployeeID(employee.EmployeeID);
            Contract contract = CreateContractDTO.ToEntity(dto);
            var fileName = System.IO.Path.GetFileName(dto.File.FileName);
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
            CalculateVacation(employee);
            if(repo.Update(employee))
            {
                
                return true;
            }
            else
            {
                return false;
            }
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
        private static void CalculateVacation(Employee employee)
        {
            int days = 0;
            DateTime start = employee.Contracts.Last().StartDate;
            DateTime end = employee.Contracts.Last().EndDate.Value;
            if (end == null || end.Year > start.Year)
            {
                // Ako je neodredjeno ili ako ugovor ide u narednu godinu
                end = new DateTime(start.Year, 12, 31);
            }
            days = CalculateVacationDaysForDates(start, end);
            employee.CurrentVacationDays += days;
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
        #endregion

    }
}