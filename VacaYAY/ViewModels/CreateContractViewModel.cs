using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;
using VacaYAY.Entities.Employees;

namespace VacaYAY.ViewModels
{
    public class CreateContractViewModel
    {
        public string Text { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public HttpPostedFileBase File { get; set; }
        public static CreateContractDTO ToDTO(CreateContractViewModel contract)
        {
            CreateContractDTO dto = new CreateContractDTO()
            {
                SerialNumber=contract.Text,
                StartDate=contract.StartDate,
                EndDate=contract.EndDate,
            };
            return dto;
        }
        public static List<CreateContractDTO> ToDTOs(List<CreateContractViewModel> contracts)
        {
            List<CreateContractDTO> dtos = new List<CreateContractDTO>();
            foreach(var contract in contracts)
            {
                CreateContractDTO dto = new CreateContractDTO()
                {
                    SerialNumber = contract.Text,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    File = contract.File,
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}