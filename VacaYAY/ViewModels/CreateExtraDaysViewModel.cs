using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;
using static VacaYAY.Common.Enums;

namespace VacaYAY.ViewModels
{
    public class CreateExtraDaysViewModel
    {
        public DateTime TimeStamp { get; set; }
        public Basis Basis { get; set; }
        [Range(1,double.MaxValue,ErrorMessage ="Number of Days must be positive")]
        public int Days { get; set; }
        public int EmployeeID { get; set; }

        public static CreateExtraDaysDTO ToDTO(CreateExtraDaysViewModel vm)
        {
            CreateExtraDaysDTO dto = new CreateExtraDaysDTO()
            {
                TimeStamp = vm.TimeStamp,
                Basis = vm.Basis,
                Days = vm.Days,
                EmployeeID=vm.EmployeeID,
            };
            return dto;
        }
    }
}