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
        public List<CollectiveEmployeeDTO> collectiveEmployees { get; set; } = new List<CollectiveEmployeeDTO>();
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
            List<DetailsRequestDTO> newList = new List<DetailsRequestDTO>();
            int i = 0;
            while (dtos.Count > 0)
            {
                if (dtos[0].TypeOfDays != TypeOfDays.Collective)
                {
                    newList.Add(dtos[0]);
                    dtos.RemoveAt(0);
                }
                else
                {
                    List<DetailsRequestDTO> collectiveGroup = new List<DetailsRequestDTO>();
                    collectiveGroup = dtos.Where(x => x.SubmissionDate == dtos[0].SubmissionDate).Where(x => x.TypeOfDays == dtos[0].TypeOfDays).ToList();
                    DetailsRequestDTO vm = DetailsRequestDTO.ToDTOGroup(collectiveGroup);
                    newList.Add(vm);
                    foreach (var item in collectiveGroup)
                    {
                        dtos.Remove(item);
                    }
                }
            }
            return newList;
        }
        public static DetailsRequestDTO ToDTOGroup(List<DetailsRequestDTO> dtos)
        {
            DetailsRequestDTO vm = new DetailsRequestDTO();
            vm = dtos[0];
            vm.collectiveEmployees.Add(CollectiveEmployeeDTO.ToDTO(dtos[0]));
            for (int i = 1; i < dtos.Count; i++)
            {
                vm.collectiveEmployees.Add(CollectiveEmployeeDTO.ToDTO(dtos[i]));
            }
            return vm;
        }
    }
}
