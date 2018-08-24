using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class CollectiveEmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int RequestID { get; set; }

        public static CollectiveEmployeeViewModel ToVM(CollectiveEmployeeDTO vm)
        {
            CollectiveEmployeeViewModel cvm = new CollectiveEmployeeViewModel()
            {
                EmployeeID = vm.EmployeeID,
                Name = vm.Name,
                RequestID=vm.RequestID,
            };
            return cvm;
        }
        public static CollectiveEmployeeViewModel ToVMForRequest(DetailsRequestEmployeeViewModel vm)
        {
            CollectiveEmployeeViewModel cvm = new CollectiveEmployeeViewModel()
            {
                EmployeeID = vm.EmployeeID,
                Name = vm.Name + " " + vm.LastName,
            };
            return cvm;
        }
        public static List<CollectiveEmployeeViewModel> ToVMs(List<CollectiveEmployeeDTO> dtos)
        {
            List<CollectiveEmployeeViewModel> vms = new List<CollectiveEmployeeViewModel>();
            foreach(var item in dtos)
            {
                vms.Add(CollectiveEmployeeViewModel.ToVM(item));
            }
            return vms;
        }
    }
}