using System;
using System.Collections.Generic;
using System.Linq;
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
            Paid,
            Unpaid,
            Yearly,
            Leftover
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
