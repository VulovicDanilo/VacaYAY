using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;
using static VacaYAY.Common.Enums;

namespace VacaYAY.ViewModels
{
    public class DetailsEmployeeExtraDaysViewModel
    {
        public int ExtraDaysID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Timestamp")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Timestamp { get; set; }
        public int Days { get; set; }
        public Basis Basis { get; set; }

        public static DetailsEmployeeExtraDaysViewModel ToVM(DetailsEmployeeExtraDaysDTO dto)
        {
            DetailsEmployeeExtraDaysViewModel vm = new DetailsEmployeeExtraDaysViewModel()
            {
                ExtraDaysID = dto.ExtraDaysID,
                Timestamp = dto.Timestamp,
                Basis = dto.Basis,
                Days = dto.Days,
            };
            return vm;
        }
        public static List<DetailsEmployeeExtraDaysViewModel> ToVMs(List<DetailsEmployeeExtraDaysDTO> dtos)
        {
            List<DetailsEmployeeExtraDaysViewModel> vms = new List<DetailsEmployeeExtraDaysViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(DetailsEmployeeExtraDaysViewModel.ToVM(dto));
            }
            return vms;
        }
    }
}