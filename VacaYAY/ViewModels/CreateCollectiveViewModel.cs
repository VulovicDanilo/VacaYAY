using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;
using static VacaYAY.Common.Enums;

namespace VacaYAY.ViewModels
{
    public class CreateCollectiveViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public TypeOfDays TypeOfDays { get; set; }

        public static CreateCollectiveDTO ToDTO(CreateCollectiveViewModel vm)
        {
            CreateCollectiveDTO dto = new CreateCollectiveDTO()
            {
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                TypeOfDays = TypeOfDays.Collective,
            };
            return dto;
        }
    }
}