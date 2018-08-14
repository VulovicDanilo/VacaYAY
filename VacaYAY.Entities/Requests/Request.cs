using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VacaYAY.Entities.Comments;
using VacaYAY.Entities.Employees;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Entities.Requests
{
    
    public class Request
    { 
        public Request()
        {
            Comments = new List<Comment>();
            
        }
        public int RequestID { get; set; }
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
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
        public Status Status { get; set; }
        [Display(Name = "Vacation Type")]
        public TypeOfDays TypeOfDays { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}