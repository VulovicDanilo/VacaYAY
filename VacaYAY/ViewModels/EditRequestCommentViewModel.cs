using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Business.DTOs;
using static VacaYAY.Common.Enums;

namespace VacaYAY.ViewModels
{
    public class EditRequestCommentViewModel
    {
        public string Text { get; set; }
        public Status Status { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Timestamp")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TimeStamp { get; set; }
        public EditRequestEmployeeViewModel Commenter { get; set; }

        public static List<EditRequestCommentViewModel> ToViewModelList(List<EditRequestCommentDTO> dtos)
        {
            List<EditRequestCommentViewModel> vms = new List<EditRequestCommentViewModel>();
            foreach(var dto in dtos)
            {
                EditRequestCommentViewModel vm = new EditRequestCommentViewModel()
                {
                    Commenter = EditRequestEmployeeViewModel.ToViewModel(dto.Commenter),
                    Status = dto.Status,
                    TimeStamp = dto.TimeStamp,
                    Text = dto.Text,
                };
                vms.Add(vm);
            };
            return vms;
        }
        public static List<EditRequestCommentDTO> ToDTOList(List<EditRequestCommentViewModel> vms)
        {
            List<EditRequestCommentDTO> dtos = new List<EditRequestCommentDTO>();
            foreach (var vm in vms)
            {
                EditRequestCommentDTO dto = new EditRequestCommentDTO()
                {
                    Commenter = EditRequestEmployeeViewModel.ToDTO(vm.Commenter),
                    Status = vm.Status,
                    TimeStamp = vm.TimeStamp,
                    Text = vm.Text,
                };
                dtos.Add(dto);
            };
            return dtos;
        }
    }

}