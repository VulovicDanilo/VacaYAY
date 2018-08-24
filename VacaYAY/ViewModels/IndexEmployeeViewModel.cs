using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class IndexEmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        public bool Active { get; set; }
        [Display(Name="Vacation Days")]
        public int CurrentVacationDays { get; set; }
        public List<IndexEmployeeContractViewModel> Contracts { get; set; } = new List<IndexEmployeeContractViewModel>();

        public static IndexEmployeeViewModel ToVM(IndexEmployeeDTO dto)
        {
            IndexEmployeeViewModel vm = new IndexEmployeeViewModel()
            {
                EmployeeID = dto.EmployeeID,
                Name = dto.Name,
                LastName = dto.LastName,
                Active=dto.Active,
                CurrentVacationDays = dto.CurrentVacationDays,
                Contracts = IndexEmployeeContractViewModel.ToVMs(dto.Contracts),
            };
            return vm;
        }
        public static List<IndexEmployeeViewModel> ToVMs(List<IndexEmployeeDTO> dtos)
        {
            List<IndexEmployeeViewModel> vms = new List<IndexEmployeeViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(IndexEmployeeViewModel.ToVM(dto));
            }
            return vms;
        }
    }
}