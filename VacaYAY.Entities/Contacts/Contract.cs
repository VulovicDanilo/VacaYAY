using System;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Entities.Employees;

namespace VacaYAY.Entities.Contracts
{
    public class Contract
    {
        public Contract()
        {

        }
        public int ContractID { get; set; }
        public string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }
        public string Link { get; set; }
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}