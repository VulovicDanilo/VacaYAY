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
        public string LastName { get; set; }
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
                IsManager = dto.IsManager,
                Contracts = EditEmployeeContractViewModel.ToVMs(dto.Contracts),
                Resolutions = EditEmployeeResolutionViewModel.ToVMs(dto.Resolutions),
            };
            return vm;
        }


    }
}