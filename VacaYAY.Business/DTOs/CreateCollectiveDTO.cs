using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Requests;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Business.DTOs
{
    public class CreateCollectiveDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TypeOfDays TypeOfDays { get; set; }
        public string ResolutionNumber { get; set; }

        public static Request ToEntity(CreateCollectiveDTO dto)
        {
            Request request = new Request()
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TypeOfDays = dto.TypeOfDays,
            };
            return request;
        }
    }
}
