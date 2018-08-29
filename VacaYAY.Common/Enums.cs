using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            [Display(Name = "Unpaid")]
            Unpaid,
            [Display(Name = "Regular")]
            Regular,
            [Display(Name = "Collective")]
            Collective,
        }
        public enum Basis
        {
            [Display(Name = "Work Contribution")]
            WorkContribution,
            [Display(Name ="Time Spent with Employer")]
            TimeSpentWithEmployer,
            [Display(Name = "Eight Degree of Professional Development")]
            EigthDegreeOfProfessionalDevelopment,
            [Display(Name = "Working Experience")]
            WorkingExperience,
            [Display(Name = "Parenthood")]
            Parenthood,
        }




    }
}
