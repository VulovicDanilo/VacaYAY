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
            if (repo.Add(emp))
            {
                CalculateVacationDays(emp);
                return repo.Update(emp);
            }
            return false;
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
            List<Contract> newContracts = CreateContractDTO.ToEntityList(dto.NewContracts, userID);
            emp.Contracts.AddRange(newContracts);
            if (emp.IsManager != dto.IsManager)
            {
                if (EditRole(userID, dto.IsManager))
                    emp.IsManager = dto.IsManager;
            }
            return repo.Update(emp);
        }

        #region helpers
        private static void CalculateVacationDays(Employee employee)
        {
            int days = 0;
            if (employee.Contracts.Count>0)
            {
                Contract last = employee.Contracts.Last();
                if (last != null)
                {
                    int numOfMonths = 0;
                    DateTime startDate = last.StartDate;
                    DateTime? endDate = last.EndDate;
                    if (!endDate.HasValue)
                    {
                        employee.CurrentVacationDays = 20 + employee.ExtraVacationDays;
                    }
                    else
                    {
                        DateTime start = startDate;
                        DateTime end = endDate.Value;
                        numOfMonths = ((end.Year - start.Year) * 12) + end.Month - start.Month;
                        days = (20 + employee.ExtraVacationDays) / 12 * numOfMonths + employee.LeftoverVacationDays;
                        employee.CurrentVacationDays = days;
                    }
                }
                else
                {
                    employee.CurrentVacationDays = 0;
                }
            }
            else
            {
                employee.CurrentVacationDays = 0;
            }
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