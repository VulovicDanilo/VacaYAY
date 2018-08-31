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
        public int RequestID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Submission Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime SubmissionDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        [Display(Name ="Number of days")]
        public int NumOfDays { get; set; }
        [Display(Name = "Type of vacation")]
        public TypeOfDays TypeOfDays { get; set; }
        [Display(Name ="Employees on vacation")]
        public List<CollectiveEmployeeViewModel> collectiveEmployees = new List<CollectiveEmployeeViewModel>();
        public Status Status { get; set; }
        [Display(Name ="Employee")]
        public DetailsRequestEmployeeViewModel Employee { get; set; }
        public List<EditRequestCommentViewModel> Comments { get; set; } = new List<EditRequestCommentViewModel>();
        [Required]
        public string Basis { get; set; }

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
                Status = dto.Status,
                collectiveEmployees = CollectiveEmployeeViewModel.ToVMs(dto.collectiveEmployees),
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
        public static DetailsRequestViewModel ToVMGroup(List<DetailsRequestViewModel> list)
        {
            DetailsRequestViewModel vm = new DetailsRequestViewModel();
            vm = list[0];
            vm.collectiveEmployees.Add(CollectiveEmployeeViewModel.ToVMForRequest(list[0].Employee));
            for (int i=1;i<list.Count;i++)
            {
                vm.collectiveEmployees.Add(CollectiveEmployeeViewModel.ToVMForRequest(list[i].Employee));
            }
            return vm;
        }
    }
}