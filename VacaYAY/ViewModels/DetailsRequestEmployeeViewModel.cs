using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class DetailsRequestEmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }    
        public string LastName { get; set; }

        public static DetailsRequestEmployeeViewModel ToViewModel(DetailsRequestEmployeeDTO dto)
        {
            DetailsRequestEmployeeViewModel vm = new DetailsRequestEmployeeViewModel()
            {
                EmployeeID = dto.EmployeeID,
                Name = dto.Name,
                LastName = dto.LastName,
            };
            return vm;
        }

        internal static DetailsRequestEmployeeDTO ToDTO(DetailsRequestEmployeeViewModel vm)
        {
            DetailsRequestEmployeeDTO dto = new DetailsRequestEmployeeDTO()
            {
                EmployeeID = vm.EmployeeID,
                Name = vm.Name,
                LastName = vm.LastName,
            };
            return dto;
        }
    }
}