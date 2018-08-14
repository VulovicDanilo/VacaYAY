using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Business.DTOs
{
    public class EditEmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsManager { get; set; }
        public List<EditEmployeeContractDTO> Contracts { get; set; }
        public List<EditEmployeeResolutionDTO> Resolutions { get; set; }

        public static EditEmployeeDTO ToDTO(Employee employee)
        {
            EditEmployeeDTO dto = new EditEmployeeDTO()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                LastName = employee.LastName,
                IsManager = employee.IsManager,
                Contracts = EditEmployeeContractDTO.ToDTOs(employee.Contracts),
            };
            return dto;

        }
        public void AddResolutions(List<Resolution> resolutions)
        {
            this.Resolutions = EditEmployeeResolutionDTO.ToDTOs(resolutions);
        }
    }
}
