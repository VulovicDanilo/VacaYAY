using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacaYAY.Entities.Comments;
using VacaYAY.Entities.Requests;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Business.DTOs
{
    public class EditRequestCommentDTO
    {
        public string Text { get; set; }
        public Status Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public EditRequestEmployeeDTO Commenter { get; set; }

        public static List<Comment> ToEntityList(List<EditRequestCommentDTO> dtos)
        {
            List<Comment> comments = new List<Comment>();
            foreach(var dto in dtos)
            {
                Comment comment = new Comment()
                {
                    Text = dto.Text,
                    Status = dto.Status,
                    Timestamp = dto.TimeStamp,
                };
                comments.Add(comment);
            }
            return comments;
        }
        public static List<EditRequestCommentDTO> ToDTOList(List<Comment> comments)
        {
            List<EditRequestCommentDTO> dtos = new List<EditRequestCommentDTO>();
            foreach(var comment in comments)
            {
                EditRequestCommentDTO dto = new EditRequestCommentDTO()
                {
                    Commenter = EditRequestEmployeeDTO.ToDTO(comment.Commenter),
                    Text = comment.Text,
                    Status = comment.Status,
                    TimeStamp = comment.Timestamp,
                };
                dtos.Add(dto);
            }
            return dtos;
        }

    }
}