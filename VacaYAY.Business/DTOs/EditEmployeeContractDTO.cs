using System;
using System.Collections.Generic;
using VacaYAY.Entities.Contracts;

namespace VacaYAY.Business.DTOs
{
    public class EditEmployeeContractDTO
    {
        public int ContractID { get; set; }
        public string SerialNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Link { get; set; }

        public static EditEmployeeContractDTO ToDTO(Contract contract)
        {
            EditEmployeeContractDTO dto = new EditEmployeeContractDTO()
            {
                ContractID = contract.ContractID,
                SerialNumber=contract.SerialNumber,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Link = contract.Link,
            };
            return dto;
        }
        public static List<EditEmployeeContractDTO> ToDTOs(List<Contract> contracts)
        {
            List<EditEmployeeContractDTO> dtos = new List<EditEmployeeContractDTO>();
            foreach(var contract in contracts)
            {
                EditEmployeeContractDTO dto = EditEmployeeContractDTO.ToDTO(contract);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}