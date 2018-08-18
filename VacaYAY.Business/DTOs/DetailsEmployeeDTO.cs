using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.ExtraDays;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Business.DTOs
{
    public class DetailsEmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        public int CurrentVacationDays { get; set; }
        public int ExtraVacationDays { get; set; }
        public int LeftoverVacationDays { get; set; }
        public bool IsManager { get; set; }
        public bool Active { get; set; }
        public List<DetailsEmployeeContractDTO> Contracts { get; set; } = new List<DetailsEmployeeContractDTO>();
        public List<ResolutionDTO> Resolutions { get; set; } = new List<ResolutionDTO>();
        public List<DetailsEmployeeExtraDaysDTO> ExtraDays { get; set; } = new List<DetailsEmployeeExtraDaysDTO>();

        public static DetailsEmployeeDTO ToDTO(Employee employee)
        {
            DetailsEmployeeDTO dto = new DetailsEmployeeDTO()
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                LastName = employee.LastName,
                City = employee.City,
                Profession = employee.Profession,
                IsManager = employee.IsManager,
                Active=employee.Active,
                ExtraVacationDays=employee.ExtraVacationDays,
                CurrentVacationDays=employee.CurrentVacationDays,
                LeftoverVacationDays=employee.LeftoverVacationDays,
                Contracts = DetailsEmployeeContractDTO.ToDTOs(employee.Contracts),
            };
            return dto;

        }
        public void AddResolutions(List<Resolution> resolutions)
        {
            this.Resolutions = ResolutionDTO.ToDTOs(resolutions);
        }
        public void AddExtraDays(List<ExtraDays> extraDays)
        {
            this.ExtraDays = DetailsEmployeeExtraDaysDTO.ToDTOs(extraDays);
        }
    }
}
