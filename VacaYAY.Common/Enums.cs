using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VacaYAY.Common
{
    public class Enums
    {
        public enum Status
        {
            Approved,
            Rejected,
            Pending
        }

        public enum TypeOfDays
        {
            [Display(Name ="Paid")]
            Paid,
            [Display(Name ="Unpaid")]
            Unpaid,
            [Display(Name ="Regular")]
            Yearly,
            [Display(Name ="Collective")]
            Collective,
        }
        public enum Basis
        {
            WorkContribution,
            TimeSpentWithEmployer,
            EightDegreeOfProfessionalDevelopment,
            WorkingExperience,
            Parenthood,
        }

        
    }
}
