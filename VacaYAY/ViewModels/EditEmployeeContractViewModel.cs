using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class EditEmployeeContractViewModel
    {
        [Key]
        public int ContractID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Link { get; set; }

        public static EditEmployeeContractViewModel ToVM(EditEmployeeContractDTO dto)
        {
            EditEmployeeContractViewModel vm = new EditEmployeeContractViewModel()
            {
                ContractID = dto.ContractID,
                SerialNumber = dto.SerialNumber,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
            };
            return vm;
        }
        public static List<EditEmployeeContractViewModel> ToVMs(List<EditEmployeeContractDTO> dtos)
        {
            List<EditEmployeeContractViewModel> vms = new List<EditEmployeeContractViewModel>();
            foreach (var dto in dtos)
            {
                EditEmployeeContractViewModel vm = EditEmployeeContractViewModel.ToVM(dto);
                vms.Add(vm);
            }
            return vms;
        }
    }
}