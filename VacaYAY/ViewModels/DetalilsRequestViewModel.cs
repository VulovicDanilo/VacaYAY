using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;
using static VacaYAY.Common.Enums;

namespace VacaYAY.ViewModels
{
    public class DetailsRequestViewModel
    {
        [Key]
        public int RequestID;
        [DataType(DataType.Date)]
        [Display(Name = "Submission Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SubmissionDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        public int NumOfDays { get; set; }
        [Display(Name = "Type of vacation")]
        public TypeOfDays TypeOfDays { get; set; }
        public Status Status { get; set; }
        public DetailsRequestEmployeeViewModel Employee { get; set; }
        public List<EditRequestCommentViewModel> Comments { get; set; } = new List<EditRequestCommentViewModel>();

        public static DetailsRequestViewModel ToVM(DetailsRequestDTO dto)
        {
            DetailsRequestViewModel vm = new DetailsRequestViewModel()
            {
                RequestID = dto.RequestID,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                SubmissionDate = dto.SubmissionDate,
                NumOfDays = dto.NumOfDays,
                TypeOfDays = dto.TypeOfDays,
                Status=dto.Status,
                Comments = EditRequestCommentViewModel.ToViewModelList(dto.Comments),
                Employee = DetailsRequestEmployeeViewModel.ToViewModel(dto.Employee),
            }; 
            return vm;
        }
        public static List<DetailsRequestViewModel> ToVMs(List<DetailsRequestDTO> dtos)
        {
            List<DetailsRequestViewModel> vms = new List<DetailsRequestViewModel>();
            foreach(var dto in dtos)
            {
                var vm = DetailsRequestViewModel.ToVM(dto);
                vms.Add(vm);
            }
            return vms;
        }
        public static DetailsRequestDTO ToDTO(DetailsRequestViewModel vm)
        {
            DetailsRequestDTO dto = new DetailsRequestDTO()
            {
                RequestID = vm.RequestID,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                SubmissionDate = vm.SubmissionDate,
                NumOfDays = vm.NumOfDays,
                TypeOfDays = vm.TypeOfDays,
                Comments = EditRequestCommentViewModel.ToDTOList(vm.Comments),
                //Employee = DetailsRequestEmployeeViewModel.ToDTO(vm.Employee),
            };
            return dto;
        }
    }
}