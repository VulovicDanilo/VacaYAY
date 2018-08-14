
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacaYAY.Entities.Requests;
using static VacaYAY.Common.Enums;
using VacaYAY.Entities.Comments;
using System;

namespace VacaYAY.Business.DTOs
{
    public class CreateRequestDTO
    {
        public int RemainingVacationDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TypeOfDays TypeOfDays { get; set; }
        public string Comment { get; set; }

        public static Request ToEntity(CreateRequestDTO dto)
        {
            Request request = new Request()
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TypeOfDays = dto.TypeOfDays,
                SubmissionDate = DateTime.Now,
                Status = Status.Pending,
            };
            Comment comment = new Comment()
            {
                Text = dto.Comment,
                Timestamp = DateTime.Now,
                Status = Status.Pending,
            };
            request.Comments.Add(comment);
            return request;
        }
    }
}