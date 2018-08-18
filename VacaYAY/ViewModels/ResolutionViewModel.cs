using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class ResolutionViewModel
    {
        [Key]
        public int ResolutionID { get; set; }
        public string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public string Link { get; set; }
        public int NumOfDays { get; set; }

        public static ResolutionViewModel ToVM(ResolutionDTO dto)
        {
            ResolutionViewModel vm = new ResolutionViewModel()
            {
                ResolutionID = dto.ResolutionID,
                SerialNumber = dto.SerialNumber,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                NumOfDays = dto.NumOfDays,
                Link=dto.Link,
            };
            return vm;
        }
        public static List<ResolutionViewModel> ToVMs(List<ResolutionDTO> dtos)
        {
            List<ResolutionViewModel> vms = new List<ResolutionViewModel>();
            foreach(var dto in dtos)
            {
                ResolutionViewModel vm = ResolutionViewModel.ToVM(dto);
                vms.Add(vm);
            }
            return vms;
        }
    }
}