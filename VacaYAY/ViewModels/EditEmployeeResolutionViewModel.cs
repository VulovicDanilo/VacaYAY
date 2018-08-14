using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;

namespace VacaYAY.ViewModels
{
    public class EditEmployeeResolutionViewModel
    {
        [Key]
        public int ResolutionID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Link { get; set; }
        public int NumOfDays { get; set; }

        public static EditEmployeeResolutionViewModel ToVM(EditEmployeeResolutionDTO dto)
        {
            EditEmployeeResolutionViewModel vm = new EditEmployeeResolutionViewModel()
            {
                ResolutionID = dto.ResolutionID,
                SerialNumber = dto.SerialNumber,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                NumOfDays = dto.NumOfDays,
            };
            return vm;
        }
        public static List<EditEmployeeResolutionViewModel> ToVMs(List<EditEmployeeResolutionDTO> dtos)
        {
            List<EditEmployeeResolutionViewModel> vms = new List<EditEmployeeResolutionViewModel>();
            foreach(var dto in dtos)
            {
                EditEmployeeResolutionViewModel vm = EditEmployeeResolutionViewModel.ToVM(dto);
                vms.Add(vm);
            }
            return vms;
        }
    }
}