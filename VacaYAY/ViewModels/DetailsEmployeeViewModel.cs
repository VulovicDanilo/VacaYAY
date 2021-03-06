﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class DetailsEmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Profession { get; set; }
        [Display(Name ="Vacation days")]
        public int CurrentVacationDays { get; set; }
        [Display(Name="Extra vacation days")]
        public int ExtraVacationDays { get; set; }
        [Display(Name ="Leftover days")]
        public int LeftoverVacationDays { get; set; }
        [Display(Name = "Used vacation days")]
        public int UsedPaidVacationDays { get; set; }
        [Display(Name ="Manager")]
        public bool IsManager { get; set; }
        public bool Active { get; set; }
        public List<DetailsEmployeeContractViewModel> Contracts { get; set; } = new List<DetailsEmployeeContractViewModel>();
        public List<ResolutionViewModel> Resolutions { get; set; } = new List<ResolutionViewModel>();
        public List<DetailsEmployeeExtraDaysViewModel> ExtraDays { get; set; } = new List<DetailsEmployeeExtraDaysViewModel>();

        public static DetailsEmployeeViewModel ToVM(DetailsEmployeeDTO dto)
        {
            DetailsEmployeeViewModel vm = new DetailsEmployeeViewModel()
            {
                EmployeeID = dto.EmployeeID,
                Name = dto.Name + " " + dto.LastName,
                City = dto.City,
                Profession = dto.Profession,
                IsManager = dto.IsManager,
                Active=dto.Active,
                ExtraVacationDays = dto.ExtraVacationDays,
                CurrentVacationDays = dto.CurrentVacationDays,
                LeftoverVacationDays = dto.LeftoverVacationDays,
                UsedPaidVacationDays=dto.UsedPaidVacationDays,
                Contracts = DetailsEmployeeContractViewModel.ToVMs(dto.Contracts),
                Resolutions = ResolutionViewModel.ToVMs(dto.Resolutions),
                ExtraDays = DetailsEmployeeExtraDaysViewModel.ToVMs(dto.ExtraDays),
            };
            return vm;

        }
    }
}