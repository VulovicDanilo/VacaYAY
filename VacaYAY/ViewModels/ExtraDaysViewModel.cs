using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;
using static VacaYAY.Common.Enums;

namespace VacaYAY.ViewModels
{
    public class ExtraDaysViewModel
    {
        [Key]
        public int ExtraDaysID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Timestamp")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Timestamp { get; set; }
        public int Days { get; set; }
        public Basis Basis { get; set; }
        [Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }
        public int EmployeeID { get; set; }

        public static ExtraDaysViewModel ToVM(ExtraDaysDTO dto)
        {
            ExtraDaysViewModel vm = new ExtraDaysViewModel()
            {
                ExtraDaysID = dto.ExtraDaysID,
                Timestamp = dto.Timestamp,
                Basis = dto.Basis,
                Days = dto.Days,
                EmployeeName=dto.EmployeeName,
                EmployeeID=dto.EmployeeID,
            };
            return vm;
        }
        public static List<ExtraDaysViewModel> ToVMs(List<ExtraDaysDTO> dtos)
        {
            List<ExtraDaysViewModel> vms = new List<ExtraDaysViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(ExtraDaysViewModel.ToVM(dto));
            }
            return vms;
        }
    }
}