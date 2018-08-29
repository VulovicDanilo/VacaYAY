using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.ExtraDays;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Business.DTOs
{
    public class CreateExtraDaysDTO
    {
        public DateTime TimeStamp { get; set; }
        public Basis Basis { get; set; }
        public int Days { get; set; }
        public int EmployeeID { get; set; }

        public static ExtraDays ToEntity(CreateExtraDaysDTO dto)
        {
            ExtraDays extraDays = new ExtraDays()
            {
                Timestamp = dto.TimeStamp,
                Basis = dto.Basis,
                Days = dto.Days,
                EmployeeID = dto.EmployeeID,
            };
            return extraDays;
        }
    }
}
