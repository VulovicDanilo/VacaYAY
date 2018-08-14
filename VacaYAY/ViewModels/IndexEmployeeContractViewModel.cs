using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class IndexEmployeeContractViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }

        public static List<IndexEmployeeContractViewModel> ToVMs(List<IndexEmployeeContractDTO> dtos)
        {
            List<IndexEmployeeContractViewModel> vms = new List<IndexEmployeeContractViewModel>();
            foreach (var dto in dtos)
            {
                IndexEmployeeContractViewModel vm = new IndexEmployeeContractViewModel()
                {
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                };
                vms.Add(vm);
            }
            return vms;
        }
    }
}