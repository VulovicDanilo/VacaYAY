﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Resolutions;
using Microsoft.AspNet.Identity.EntityFramework;
using VacaYAY.Common;

namespace VacaYAY.Data.Repos
{
    public class EmployeeRepository
    {
        private ApplicationDbContext db { get; set; }

        public EmployeeRepository()
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Creates new <see cref="Employee"/>.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool Add(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Finds <see cref="Employee"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee Find(int? id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                return employee;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Updates <see cref="Employee"/>.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool Update(Employee employee)
        {
            try
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Returns a list of all <see cref="Employee"/>.
        /// </summary>
        /// <returns></returns>
        public List<Employee> All()
        {
            try
            {
                return db.Employees.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Gets all active <see cref="Employee"></see>'s./>
        /// </summary>
        /// <returns></returns>
        public List<Employee> AllActiveEmployees()
        {
            try
            {
                return db.Employees.Where(x => x.Active).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Gets all inactive <see cref="Employee"></see>'s./>
        /// </summary>
        /// <returns></returns>
        public List<Employee> AllInactiveEmployees()
        {
            try
            {
                return db.Employees.Where(x => x.Active == false).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Get <see cref="Employee"/>'s with certain name and last name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public List<Employee> GetWithNameOrSurname(params string[] words)
        {
            try
            {
                var dict = new Dictionary<int, Employee>();
                foreach(var word in words)
                {
                    List<Employee> list = db.Employees.Where(x => x.Name.Contains(word)).ToList();
                    foreach (var employee in list) dict[employee.EmployeeID] = employee;
                    list = db.Employees.Where(x => x.LastName.Contains(word)).ToList();
                    foreach (var employee in list) dict[employee.EmployeeID] = employee;
                }
                return dict.Values.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Get <see cref="Employee"/>'s who started working after specified date.
        /// </summary>
        public List<Employee> GetEmployeesWhoStartedWorkingAfter(DateTime date)
        {
            // TODO GetByDate() in EmployeeRepository
            try
            {
                return db.Employees
                    .Where(x => x.Contracts.Count > 0)
                    .Where(x => x.Contracts.FirstOrDefault().StartDate > date)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Get <see cref="Employee"/>'s with leftover days.
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetWithLeftoverDays()
        {
            try
            {
                return db.Employees.Where(x => x.LeftoverVacationDays > 0).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Get <see cref="Employee"/>'s resolutions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Resolution> GetUsersResolutions(int? id)
        {
            try
            {
                return db.Employees.Find(id).Resolutions.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Get <see cref="Employee"/> by user ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetEmployeeByUserID(string id)
        {
            try
            {
                return db.Employees.Where(x => x.User.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Get <see cref="Employee"/>'s ID with Users ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetEmployeeIDByUserID(string id)
        {
            try
            {
                return db.Employees.Where(x => x.User.Id == id).Select(x => x.EmployeeID).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        /// <summary>
        /// Get emails of all managers.
        /// </summary>
        /// <returns></returns>
        public List<string> GetManagerEmails()
        {
            try
            {
                return db.Employees.
                    Where(x => x.IsManager)
                    .Select(x => x.User.Email)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Gets users email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserEmail(string id)
        {
            try
            {
                return db.Employees.
                    Where(x => x.User.Id == id)
                    .Select(x => x.User.Email).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Soft delets <see cref="Employee"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SoftDelete(int? id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                if (employee != null)
                {
                    employee.Active = false;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Restores <see cref="Employee"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Restore(int? id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                if (employee != null)
                {
                    employee.Active = true;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Gets Users vacation days.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUsersVacationDays(string id)
        {
            try
            {
                return db.Employees.
                    Where(x => x.User.Id == id)
                    .Select(x => x.CurrentVacationDays).First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public string GetUserIDWithEmployeeID(int? id)
        {
            try
            {
                return db.Employees.
                    Where(x => x.EmployeeID == id)
                    .Select(x => x.UserID).First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
        /// <summary>
        /// Deletes <see cref="Employee"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int? id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
