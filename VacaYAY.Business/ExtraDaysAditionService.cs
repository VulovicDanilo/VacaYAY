using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacaYAY.Data.Repos;
using VacaYAY.Entities.ExtraDaysAditions;

namespace VacaYAY.Business
{
    public class ExtraDaysAditionService
    {
        private static ExtraDaysAditionRepository repo = new ExtraDaysAditionRepository();

        public static bool Add(ExtraDaysAdition extraDaysAdition)
        {
            return repo.Add(extraDaysAdition);
        }
    }
}