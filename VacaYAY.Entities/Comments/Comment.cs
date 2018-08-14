using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Entities.Comments
{
    public class Comment
    {
        public Comment()
        {
            
        }
        public int CommentID { get; set; }
        [Required]
        public string Text { get; set; }

        #region TimeStamp annotations
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        #endregion
        public DateTime Timestamp { get; set; }
        public Status Status { get; set; }
        [ForeignKey("Commenter")]
        public int? CommenterID { get; set; }
        public virtual Employee Commenter { get; set; }
        public int RequestID { get; set; }
        public virtual Request Request { get; set; }

    }
}