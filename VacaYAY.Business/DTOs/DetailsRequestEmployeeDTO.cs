using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Employees;

namespace VacaYAY.Business.DTOs
{
    public class DetailsRequestEmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public static DetailsRequestEmployeeDTO ToDTO(Employee employee)
        {
            DetailsRequestEmployeeDTO dto = new DetailsRequestEmployeeDTO()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                LastName = employee.LastName,
            };
            return dto;
        }
    }
}
