using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VacaYAY.Data;
using VacaYAY.Entities;
using VacaYAY.Entities.Employees;

namespace VacaYAY.Business.DTOs
{
    public class RegisterEmployeeDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        public bool IsManager { get; set; }
        public List<CreateContractDTO> Contracts { get; set; }
        
        public static Employee ToEntity(RegisterEmployeeDTO dto)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(store);
            var user = new ApplicationUser() { Email = dto.Email, UserName = dto.Email };
            var usmanger = _userManager.Create(user, dto.Password);
            if (usmanger.Succeeded)
            {
                var u = _userManager.FindByEmail(dto.Email);
                if (dto.IsManager)
                {
                    _userManager.AddToRole(u.Id, "Manager");
                }
                else
                    _userManager.AddToRole(u.Id, "Employee");
                Employee employee = new Employee()
                {
                    Name = dto.Name,
                    LastName = dto.LastName,
                    City=dto.City,
                    Profession=dto.Profession,
                    IsManager = dto.IsManager,
                    UserID = u.Id,
                };
                return employee;
            }
            return null;
        }
    }
}