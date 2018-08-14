using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class RegisterEmployeeViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Lozinka")]
        public string Password { get; set; }
        [Required]
        [Display(Name="Ime")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Prezime")]
        public string LastName { get; set; }
        [Display(Name="Manager")]
        public bool isManager { get; set; }
        public List<CreateContractViewModel> Contracts { get; set; }

        public static RegisterEmployeeDTO ToDTO(RegisterEmployeeViewModel employee)
        {
            RegisterEmployeeDTO dto = new RegisterEmployeeDTO()
            {
                Email = employee.Email,
                Name = employee.Name,
                LastName = employee.LastName,
                Password = employee.Password,
                IsManager = employee.isManager,
                Contracts = CreateContractViewModel.ToDTOs(employee.Contracts),
            };
            return dto;
        }
    }
}