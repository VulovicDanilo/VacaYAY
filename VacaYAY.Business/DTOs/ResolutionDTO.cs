using System;
using System.Collections.Generic;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Business.DTOs
{
    public class ResolutionDTO
    {
        public int ResolutionID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfDays { get; set; }
        public string Link { get; set; }

        public static ResolutionDTO ToDTO(Resolution resolution)
        {
            ResolutionDTO dto = new ResolutionDTO()
            {
                ResolutionID = resolution.ResolutionID,
                SerialNumber = resolution.SerialNumber,
                StartDate = resolution.Request.StartDate,
                EndDate = resolution.Request.EndDate,
                NumOfDays = resolution.Request.NumberOfDays,
                Link=resolution.Link,
            };
            return dto;
        }
        public static List<ResolutionDTO> ToDTOs(List<Resolution> resolutions)
        {
            List<ResolutionDTO> dtos = new List<ResolutionDTO>();
            foreach( var resolution in resolutions)
            {
                ResolutionDTO dto = ResolutionDTO.ToDTO(resolution);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}