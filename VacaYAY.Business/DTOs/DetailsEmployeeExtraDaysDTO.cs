using System;
using System.Collections.Generic;
using VacaYAY.Entities.ExtraDays;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Business.DTOs
{
    public class DetailsEmployeeExtraDaysDTO
    {
        public int ExtraDaysID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Days { get; set; }
        public Basis Basis { get; set; }

        public static DetailsEmployeeExtraDaysDTO ToDTO(ExtraDays extraDays)
        {
            DetailsEmployeeExtraDaysDTO dto = new DetailsEmployeeExtraDaysDTO()
            {
                ExtraDaysID = extraDays.ExtraDaysID,
                Timestamp = extraDays.Timestamp,
                Basis = extraDays.Basis,
                Days = extraDays.Days,
            };
            return dto;
        }
        public static List<DetailsEmployeeExtraDaysDTO> ToDTOs(List<ExtraDays> extraDaysList)
        {
            List<DetailsEmployeeExtraDaysDTO> dtos = new List<DetailsEmployeeExtraDaysDTO>();
            foreach(var extraDays in extraDaysList)
            {
                dtos.Add(DetailsEmployeeExtraDaysDTO.ToDTO(extraDays));
            }
            return dtos;
        }

    }
}