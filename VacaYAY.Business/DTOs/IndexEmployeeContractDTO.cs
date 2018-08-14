using System;
using System.Collections.Generic;
using VacaYAY.Entities.Contracts;

namespace VacaYAY.Business.DTOs
{
    public class IndexEmployeeContractDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public static List<IndexEmployeeContractDTO> ToDTOs(List<Contract> contracts)
        {
            List<IndexEmployeeContractDTO> dtos = new List<IndexEmployeeContractDTO>();
            foreach(var contract in contracts)
            {
                IndexEmployeeContractDTO dto = new IndexEmployeeContractDTO()
                {
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}