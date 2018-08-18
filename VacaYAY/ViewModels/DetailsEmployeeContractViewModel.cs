using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class DetailsEmployeeContractViewModel
    {
        [Key]
        public int ContractID { get; set; }
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

        public static DetailsEmployeeContractViewModel ToVM(DetailsEmployeeContractDTO dto)
        {
            DetailsEmployeeContractViewModel vm = new DetailsEmployeeContractViewModel()
            {
                ContractID = dto.ContractID,
                SerialNumber = dto.SerialNumber,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Link = dto.Link,
            };
            return vm;
        }
        public static List<DetailsEmployeeContractViewModel> ToVMs(List<DetailsEmployeeContractDTO> dtos)
        {
            List<DetailsEmployeeContractViewModel> vms = new List<DetailsEmployeeContractViewModel>();
            foreach (var dto in dtos)
            {
                DetailsEmployeeContractViewModel vm = DetailsEmployeeContractViewModel.ToVM(dto);
                vms.Add(vm);
            }
            return vms;
        }
    }
}