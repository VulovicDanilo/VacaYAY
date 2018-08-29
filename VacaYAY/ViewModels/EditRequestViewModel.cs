using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business;
using VacaYAY.Business.DTOs;
using VacaYAY.Entities.Comments;
using static VacaYAY.Common.Enums;

namespace VacaYAY.ViewModels
{
    public class EditRequestViewModel
    {
        [Key]
        public int RequestID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        [Display(Name ="Type of vacation")]
        public TypeOfDays TypeOfDays{ get; set; }
        public List<EditRequestCommentViewModel> Comments { get; set; } = new List<EditRequestCommentViewModel>();
        [Display(Name ="New Comment")]
        public string NewComment { get; set; }
        public int EmployeeID { get; set; }

        public static EditRequestViewModel ToViewModel(EditRequestDTO dto)
        {
            EditRequestViewModel vm = new EditRequestViewModel()
            {
                RequestID=dto.RequestID,
                StartDate=dto.StartDate,
                EndDate=dto.EndDate,
                TypeOfDays=dto.TypeOfDays,
                Comments=EditRequestCommentViewModel.ToViewModelList(dto.Comments),
                EmployeeID=dto.EmployeeID,
            };
            return vm;
        }
        public static EditRequestDTO ToDTO(EditRequestViewModel vm)
        {
            EditRequestDTO dto = new EditRequestDTO()
            {
                RequestID = vm.RequestID,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                TypeOfDays = vm.TypeOfDays,
                Comments = EditRequestCommentViewModel.ToDTOList(vm.Comments),
                EmployeeID=vm.EmployeeID,
            };
            if (!string.IsNullOrEmpty(vm.NewComment))
            {
                dto.NewComment = vm.NewComment;
            }
            return dto;
        }
        public bool IsCreatorManager(int RequestID)
        {
            return RequestService.IsCreatorManager(RequestID);
        }
    }
}