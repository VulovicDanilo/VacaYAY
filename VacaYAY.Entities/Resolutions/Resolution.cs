using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;

namespace VacaYAY.Entities.Resolutions
{
    public class Resolution
    {
        public int ResolutionID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Approval Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ApprovalDate { get; set; }
        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }
        public string Link { get; set; }
        public string Basis { get; set; }
        public int RegularUsed { get; set; }
        public int LeftoverUsed { get; set; }
        public int RequestID { get; set; }
        public virtual Request Request { get; set; }
        [ForeignKey("HR")]
        public int? HR_ID { get; set; }
        public virtual Employee HR { get; set; }
    }
}