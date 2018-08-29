using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VacaYAY.Entities.Employees;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Entities.ExtraDays
{
    
    public class ExtraDays
    {
        public int ExtraDaysID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Days { get; set; }
        public Basis Basis { get; set; }
        public int? EmployeeID { get; set; }
        [InverseProperty("ExtraDays")]
        public virtual Employee Employee { get; set; }
    }
}