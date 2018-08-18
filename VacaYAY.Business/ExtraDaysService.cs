using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacaYAY.Data.Repos;
using VacaYAY.Entities.ExtraDays;

namespace VacaYAY.Business
{
    public class ExtraDaysService
    {
        private static ExtraDaysRepository repo = new ExtraDaysRepository();

        public static bool Add(ExtraDays extraDaysAdition)
        {
            return repo.Add(extraDaysAdition);
        }
        public static List<ExtraDays> GetEmployeesExtraDays(int? id)
        {
            return repo.GetEmployeesExtraDays(id);
        }
    }
}