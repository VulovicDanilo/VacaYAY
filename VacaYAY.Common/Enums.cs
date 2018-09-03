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
            [Description("doprinosa u radu")]
            WorkContribution,
            [Display(Name ="Time Spent with Employer")]
            [Description("vremena provedenog sa poslodavcem")]
            TimeSpentWithEmployer,
            [Display(Name = "Eight Degree of Professional Development")]
            [Description("posedovanja 8. stepena obrazovanja")]
            EigthDegreeOfProfessionalDevelopment,
            [Display(Name = "Working Experience")]
            [Description("radnog iskustva")]
            WorkingExperience,
            [Display(Name = "Parenthood")]
            [Description("roditeljstva")]
            Parenthood,
        }
    }
    public static class EnumHelper
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
