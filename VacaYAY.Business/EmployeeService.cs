﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;
using VacaYAY.Data;
using VacaYAY.Data.Repos;
using VacaYAY.Entities;
using VacaYAY.Entities.Contracts;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;
using static VacaYAY.Common.Enums;

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
        public static bool RegisterEmployee(RegisterEmployeeDTO employee)
        {
            Employee emp = RegisterEmployeeDTO.ToEntity(employee);
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
        public static void CalculateVacationDays(Employee employee)
        {
            int days = 0;
            if (employee.Contracts != null)
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
    }
}