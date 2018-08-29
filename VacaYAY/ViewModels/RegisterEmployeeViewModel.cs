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
        [Display(Name="Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name="Name")]
        [RegularExpression("[a-zA-ZšŠčČćĆđĐžŽ]+",ErrorMessage ="Invalid characters used")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Last Name")]
        [RegularExpression("[a-zA-ZšŠčČćĆđĐžŽ]+", ErrorMessage = "Invalid characters used")]
        public string LastName { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        [Display(Name="Manager")]
        public bool isManager { get; set; }

        public static RegisterEmployeeDTO ToDTO(RegisterEmployeeViewModel employee)
        {
            RegisterEmployeeDTO dto = new RegisterEmployeeDTO()
            {
                Email = employee.Email,
                Password = employee.Password,
                Name = employee.Name,
                LastName = employee.LastName,
                City=employee.City,
                Profession=employee.Profession,
                IsManager = employee.isManager,
            };
            return dto;
        }
    }
}