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
        [Required]
        [Display(Name ="Serial Number")]
        public string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }
        public int EmployeeID { get; set; }
        public HttpPostedFileBase File { get; set; }
        public static CreateContractDTO ToDTO(CreateContractViewModel contract)
        {
            CreateContractDTO dto = new CreateContractDTO()
            {
                SerialNumber=contract.SerialNumber,
                StartDate=contract.StartDate,
                EndDate=contract.EndDate,
                EmployeeID=contract.EmployeeID,
                File=contract.File,
            };
            return dto;
        }
        public static List<CreateContractDTO> ToDTOs(List<CreateContractViewModel> contracts)
        {
            List<CreateContractDTO> dtos = new List<CreateContractDTO>();
            foreach(var contract in contracts)
            {
                CreateContractDTO dto = CreateContractViewModel.ToDTO(contract);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}