using System;
using System.Collections.Generic;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Business.DTOs
{
    public class EditEmployeeResolutionDTO
    {
        public int ResolutionID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfDays { get; set; }
        public string Link { get; set; }

        public static EditEmployeeResolutionDTO ToDTO(Resolution resolution)
        {
            EditEmployeeResolutionDTO dto = new EditEmployeeResolutionDTO()
            {
                ResolutionID = resolution.ResolutionID,
                SerialNumber = resolution.SerialNumber,
                StartDate = resolution.Request.StartDate,
                EndDate = resolution.Request.EndDate,
                NumOfDays = resolution.Request.NumberOfDays,
            };
            return dto;
        }
        public static List<EditEmployeeResolutionDTO> ToDTOs(List<Resolution> resolutions)
        {
            List<EditEmployeeResolutionDTO> dtos = new List<EditEmployeeResolutionDTO>();
            foreach( var resolution in resolutions)
            {
                EditEmployeeResolutionDTO dto = EditEmployeeResolutionDTO.ToDTO(resolution);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}