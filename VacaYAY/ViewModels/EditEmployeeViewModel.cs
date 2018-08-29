using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class EditEmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        [Display(Name= "Last name")]
        public string LastName { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        [Display(Name="Vacation Days")]
        [Range(0,Double.PositiveInfinity)]
        public int VacationDays { get; set; }
        [Display(Name="Leftover Days")]
        [Range(0, Double.PositiveInfinity)]
        public int LeftoverDays { get; set; }
        public bool IsHimself { get; set; }
        [Display(Name="Manager")]
        public bool IsManager { get; set; }
        public List<EditEmployeeContractViewModel> Contracts { get; set; }
        public List<ResolutionViewModel> Resolutions { get; set; }
        public List<ExtraDaysViewModel> ExtraDays { get; set; }

        public static EditEmployeeViewModel ToVM(EditEmployeeDTO dto)
        {
            EditEmployeeViewModel vm = new EditEmployeeViewModel()
            {
                EmployeeID = dto.EmployeeID,
                Name = dto.Name,
                LastName = dto.LastName,
                City = dto.City,
                Profession = dto.Profession,
                IsManager = dto.IsManager,
                IsHimself = false,
                Contracts = EditEmployeeContractViewModel.ToVMs(dto.Contracts),
                Resolutions = ResolutionViewModel.ToVMs(dto.Resolutions),
                ExtraDays = ExtraDaysViewModel.ToVMs(dto.ExtraDays),
            };
            return vm;
        }
        public static EditEmployeeDTO ToDTO(EditEmployeeViewModel vm)
        {
            EditEmployeeDTO dto = new EditEmployeeDTO()
            {
                EmployeeID = vm.EmployeeID,
                Name = vm.Name,
                LastName = vm.LastName,
                City = vm.City,
                Profession = vm.Profession,
                VacationDays=vm.VacationDays,
                LeftoverDays=vm.LeftoverDays,
                IsManager = vm.IsManager,
                //Contracts = EditEmployeeContractViewModel.ToDTOs(vm.Contracts),
                //Resolutions=EditEmployeeResolutionViewModel.ToDTOs(vm.Resolutions),
            };
            dto.Contracts = new List<EditEmployeeContractDTO>();
            return dto;
        }

    }
}