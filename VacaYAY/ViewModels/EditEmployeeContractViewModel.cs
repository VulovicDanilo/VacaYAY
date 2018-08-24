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
        [Display(Name ="Serial Number")]
        public string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
                Link = dto.Link,
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
        public static EditEmployeeContractDTO ToDTO(EditEmployeeContractViewModel vm)
        {
            EditEmployeeContractDTO dto = new EditEmployeeContractDTO()
            {
                ContractID = vm.ContractID,
                SerialNumber = vm.SerialNumber,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                Link = vm.Link,
            };
            return dto;
        }
        public static List<EditEmployeeContractDTO> ToDTOs(List<EditEmployeeContractViewModel> vms)
        {
            List<EditEmployeeContractDTO> dtos = new List<EditEmployeeContractDTO>();
            foreach(var vm in vms)
            {
                EditEmployeeContractDTO dto = EditEmployeeContractViewModel.ToDTO(vm);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}