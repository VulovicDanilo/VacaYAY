using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Business.DTOs
{
    public class PaidReportDTO
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        public int Days { get; set; }
        public string Basis { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SubmissionDate { get; set; }

        public PaidReportDTO(Employee employee,Request request,Resolution resolution)
        {
            SerialNumber = resolution.SerialNumber;
            Name = employee.Name + " " + employee.LastName;
            City = employee.City;
            Profession = employee.Profession;
            Days = request.NumberOfDays;
            Basis = resolution.Basis;
            StartDate = request.StartDate.ToString("dd.MM.yyyy.");
            EndDate = request.EndDate.ToString("dd.MM.yyyy.");
            SubmissionDate = request.SubmissionDate.ToString("dd.MM.yyyy");
        }
    }
}
