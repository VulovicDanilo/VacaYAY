using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VacaYAY.Entities.Contracts;
using VacaYAY.Entities.ExtraDaysAditions;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Entities.Employees
{
    public class Employee
    {
        public Employee()
        {
            Contracts = new List<Contract>();
            Requests = new List<Request>();
            Resolutions = new List<Resolution>();
            ExtraDaysAditions = new List<ExtraDaysAdition>();
            Active = true;
        }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public bool Active { get; set; }
        [Display(Name = "Extra Vacation Days")] // Dodatni
        public int ExtraVacationDays { get; set; }
        [Display(Name = "Total Extra Vacation Days")] // Total extra days
        public int TotalExtraVacationDays { get; set; }
        [Display(Name = "Current Vacation Days")] // Godisnje
        public int CurrentVacationDays { get; set; }
        [Display(Name = "Leftover Vacation Days")] // Zaostali od prosle godine
        public int LeftoverVacationDays { get; set; }
        [Display(Name = "Manager")]
        public bool IsManager { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        public virtual List<Contract> Contracts { get; set; }
        public virtual List<Request> Requests { get; set; }
        public virtual List<Resolution> Resolutions { get; set; }
        [InverseProperty("Employee")]
        public virtual List<ExtraDaysAdition> ExtraDaysAditions { get; set; }
    }
}