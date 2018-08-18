using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Employees;

namespace VacaYAY.Business.DTOs
{
    public class IndexEmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public int CurrentVacationDays { get; set; }
        public List<IndexEmployeeContractDTO> Contracts { get; set; } = new List<IndexEmployeeContractDTO>();

        public static IndexEmployeeDTO ToDTO(Employee employee)
        {
            IndexEmployeeDTO dto = new IndexEmployeeDTO()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                LastName = employee.LastName,
                Active=employee.Active,
                CurrentVacationDays = employee.CurrentVacationDays,
                Contracts = IndexEmployeeContractDTO.ToDTOs(employee.Contracts),
            };
            return dto;
        }
        public static List<IndexEmployeeDTO> ToDTOs(List<Employee> employees)
        {
            List<IndexEmployeeDTO> dtos = new List<IndexEmployeeDTO>();
            foreach(var employee in employees)
            {
                dtos.Add(IndexEmployeeDTO.ToDTO(employee));
            }
            return dtos;
        }
    }
}
