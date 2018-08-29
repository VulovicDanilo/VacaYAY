using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class EditVacationDaysViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        [Display(Name = "Extra Vacation Days")] // Dodatni
        [Range(0,double.MaxValue,ErrorMessage ="Value can't be negative")]
        public int ExtraVacationDays { get; set; }
        [Display(Name = "Used Paid Vacation Days")] // Paid vac days
        [Range(0, double.MaxValue, ErrorMessage = "Value can't be negative")]
        public int UsedPaidVacationDays { get; set; }
        [Display(Name = "Current Vacation Days")] // Godisnje
        [Range(0, double.MaxValue, ErrorMessage = "Value can't be negative")]
        public int CurrentVacationDays { get; set; }
        [Display(Name = "Leftover Vacation Days")] // Zaostali od prosle godine
        [Range(0, double.MaxValue, ErrorMessage = "Value can't be negative")]
        public int LeftoverVacationDays { get; set; }

        public static EditVacationDaysViewModel ToVM(EditVacationDaysDTO dto)
        {
            EditVacationDaysViewModel vm = new EditVacationDaysViewModel()
            {
                EmployeeID = dto.EmployeeID,
                CurrentVacationDays = dto.CurrentVacationDays,
                ExtraVacationDays = dto.ExtraVacationDays,
                LeftoverVacationDays = dto.LeftoverVacationDays,
                UsedPaidVacationDays = dto.UsedPaidVacationDays,
            };
            return vm;
        }
        public static EditVacationDaysDTO FromVM(EditVacationDaysViewModel vm)
        {
            EditVacationDaysDTO dto = new EditVacationDaysDTO()
            {
                EmployeeID = vm.EmployeeID,
                CurrentVacationDays = vm.CurrentVacationDays,
                ExtraVacationDays = vm.ExtraVacationDays,
                LeftoverVacationDays = vm.LeftoverVacationDays,
                UsedPaidVacationDays = vm.UsedPaidVacationDays,
            };
            return dto;
        }
    }
}