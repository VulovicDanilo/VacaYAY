using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VacaYAY.Entities.Employees;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Entities.ExtraDaysAditions
{
    
    public class ExtraDaysAdition
    {
        public int ExtraDaysAditionID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Days { get; set; }
        public Basis Basis { get; set; }
        [ForeignKey("HR")]
        public int HR_ID { get; set; }
        public virtual Employee HR { get; set; }
        public int EmployeeID { get; set; }
        [InverseProperty("ExtraDaysAditions")]
        public virtual Employee Employee { get; set; }
    }
}