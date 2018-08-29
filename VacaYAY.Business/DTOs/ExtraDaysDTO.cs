using System;
using System.Collections.Generic;
using VacaYAY.Entities.ExtraDays;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Business.DTOs
{
    public class ExtraDaysDTO
    {
        public int ExtraDaysID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Days { get; set; }
        public Basis Basis { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeID { get; set; }

        public static ExtraDaysDTO ToDTO(ExtraDays extraDays)
        {
            ExtraDaysDTO dto = new ExtraDaysDTO()
            {
                ExtraDaysID = extraDays.ExtraDaysID,
                Timestamp = extraDays.Timestamp,
                Basis = extraDays.Basis,
                Days = extraDays.Days,
                EmployeeName=extraDays.Employee.Name+ " " + extraDays.Employee.LastName,
                EmployeeID=extraDays.Employee.EmployeeID,
            };
            return dto;
        }
        public static List<ExtraDaysDTO> ToDTOs(List<ExtraDays> extraDaysList)
        {
            List<ExtraDaysDTO> dtos = new List<ExtraDaysDTO>();
            foreach (var extraDays in extraDaysList)
            {
                dtos.Add(ExtraDaysDTO.ToDTO(extraDays));
            }
            return dtos;
        }
    }
}