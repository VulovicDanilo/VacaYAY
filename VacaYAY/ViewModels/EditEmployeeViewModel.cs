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
        [Display(Name="Manager")]
        public bool IsManager { get; set; }
        public List<EditEmployeeContractViewModel> Contracts { get; set; }
        public List<EditEmployeeResolutionViewModel> Resolutions { get; set; }

        public static EditEmployeeViewModel ToVM(EditEmployeeDTO dto)
        {
            EditEmployeeViewModel vm = new EditEmployeeViewModel()
            {
                EmployeeID = dto.EmployeeID,
                Name = dto.Name,
                LastName = dto.LastName,
                City=dto.City,
                Profession=dto.Profession,
                IsManager = dto.IsManager,
                Contracts = EditEmployeeContractViewModel.ToVMs(dto.Contracts),
                Resolutions = EditEmployeeResolutionViewModel.ToVMs(dto.Resolutions),
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
                IsManager = vm.IsManager,
                //Contracts = EditEmployeeContractViewModel.ToDTOs(vm.Contracts),
                //Resolutions=EditEmployeeResolutionViewModel.ToDTOs(vm.Resolutions),
            };
            return dto;
        }

    }
}