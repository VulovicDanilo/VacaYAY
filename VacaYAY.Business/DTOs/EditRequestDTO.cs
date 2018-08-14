using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VacaYAY.Entities.Comments;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using Microsoft.AspNet.Identity;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Business.DTOs
{
    public class EditRequestDTO
    {
        public int RequestID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TypeOfDays TypeOfDays { get; set; }
        public List<EditRequestCommentDTO> Comments { get; set; }
        public string NewComment { get; set; }

        public static Request ToEntity(EditRequestDTO dto)
        {
            Request request=RequestService.GetRequest(dto.RequestID);
            request.StartDate = dto.StartDate;
            request.EndDate = dto.EndDate;
            request.NumberOfDays = RequestService.CalculateNumberOfWorkingDays(request.StartDate, request.EndDate);
            request.TypeOfDays = dto.TypeOfDays;
            if (!string.IsNullOrEmpty(dto.NewComment))
            {
                Comment comment = new Comment()
                {
                    Text = dto.NewComment,
                    Status = request.Status,
                    Timestamp = DateTime.Now,
                };
                Employee employee = EmployeeService.GetEmployeeWithUserID(HttpContext.Current.User.Identity.GetUserId());

                comment.CommenterID = employee.EmployeeID;
                request.Comments.Add(comment);
            }
            return request;
        }
        public static EditRequestDTO ToDTO(Request request)
        {
            EditRequestDTO dto = new EditRequestDTO()
            {
                RequestID = request.RequestID,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TypeOfDays = request.TypeOfDays,
                Comments = EditRequestCommentDTO.ToDTOList(request.Comments),
            };
            return dto;
        }
    }
}