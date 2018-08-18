using System;
using System.Collections.Generic;
using VacaYAY.Entities.Contracts;

namespace VacaYAY.Business.DTOs
{
    public class DetailsEmployeeContractDTO
    {
        public int ContractID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Link { get; set; }

        public static DetailsEmployeeContractDTO ToDTO(Contract contract)
        {
            DetailsEmployeeContractDTO dto = new DetailsEmployeeContractDTO()
            {
                ContractID = contract.ContractID,
                SerialNumber = contract.SerialNumber,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Link = contract.Link,
            };
            return dto;
        }
        public static List<DetailsEmployeeContractDTO> ToDTOs(List<Contract> contracts)
        {
            List<DetailsEmployeeContractDTO> dtos = new List<DetailsEmployeeContractDTO>();
            foreach (var contract in contracts)
            {
                DetailsEmployeeContractDTO dto = DetailsEmployeeContractDTO.ToDTO(contract);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}