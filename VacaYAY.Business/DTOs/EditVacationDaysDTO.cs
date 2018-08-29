using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Employees;

namespace VacaYAY.Business.DTOs
{
    public class EditVacationDaysDTO
    {
        public int EmployeeID { get; set; }
        public int ExtraVacationDays { get; set; }
        public int UsedPaidVacationDays { get; set; }
        public int CurrentVacationDays { get; set; } 
        public int LeftoverVacationDays { get; set; }

        public static EditVacationDaysDTO ToDTO(Employee employee)
        {
            EditVacationDaysDTO dto = new EditVacationDaysDTO()
            {
                EmployeeID = employee.EmployeeID,
                CurrentVacationDays = employee.CurrentVacationDays,
                ExtraVacationDays = employee.ExtraVacationDays,
                LeftoverVacationDays = employee.LeftoverVacationDays,
                UsedPaidVacationDays = employee.UsedPaidVacationDays,
            };
            return dto;
        }
    }
}
