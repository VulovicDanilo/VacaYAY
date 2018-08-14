using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Requests;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Business.DTOs
{
    public class DetailsRequestDTO
    {
        public int RequestID { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfDays { get; set; }
        public TypeOfDays TypeOfDays { get; set; }
        public Status Status { get; set; }
        public DetailsRequestEmployeeDTO Employee { get; set; }
        public List<EditRequestCommentDTO> Comments { get; set; }


        public static DetailsRequestDTO ToDTO(Request request)
        {
            DetailsRequestDTO dto = new DetailsRequestDTO()
            {
                RequestID = request.RequestID,
                SubmissionDate = request.SubmissionDate,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                NumOfDays = request.NumberOfDays,
                TypeOfDays = request.TypeOfDays,
                Status = request.Status,
                Employee = DetailsRequestEmployeeDTO.ToDTO(request.Employee),
                Comments = EditRequestCommentDTO.ToDTOList(request.Comments),
            };
            return dto;
        }
        public static List<DetailsRequestDTO> ToDTOs(List<Request> requests)
        {
            List<DetailsRequestDTO> dtos = new List<DetailsRequestDTO>();
            foreach(var request in requests)
            {
                DetailsRequestDTO dto = DetailsRequestDTO.ToDTO(request);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
