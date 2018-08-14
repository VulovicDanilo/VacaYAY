using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class EditRequestEmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsManager { get; set; }

        public static EditRequestEmployeeViewModel ToViewModel(EditRequestEmployeeDTO dto)
        {
            EditRequestEmployeeViewModel vm = new EditRequestEmployeeViewModel()
            {
                EmployeeID = dto.EmployeeID,
                Name = dto.Name,
                LastName = dto.LastName,
                IsManager = dto.IsManager,
            };
            return vm;
        }
        public static EditRequestEmployeeDTO ToDTO(EditRequestEmployeeViewModel vm)
        {
            EditRequestEmployeeDTO dto = new EditRequestEmployeeDTO()
            {
                EmployeeID = vm.EmployeeID,
                Name = vm.Name,
                LastName = vm.LastName,
                IsManager = vm.IsManager,
            };
            return dto;
        }
    }
}