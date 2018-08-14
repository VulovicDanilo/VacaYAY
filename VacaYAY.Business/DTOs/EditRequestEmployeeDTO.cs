using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Entities.Employees;

namespace VacaYAY.Business.DTOs
{
    public class EditRequestEmployeeDTO
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsManager { get; set; }

        public static EditRequestEmployeeDTO ToDTO(Employee employee)
        {
            EditRequestEmployeeDTO dto = new EditRequestEmployeeDTO()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                LastName = employee.LastName,
                IsManager = employee.IsManager,
            };
            return dto;
        }
    }
}