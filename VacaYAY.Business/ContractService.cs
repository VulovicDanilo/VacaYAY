using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacaYAY.Business.DTOs;
using VacaYAY.Data.Repos;
using VacaYAY.Entities.Contracts;
using VacaYAY.Entities.Employees;

namespace VacaYAY.Business
{
    public class ContractService
    {
        private static ContractRepository repo = new ContractRepository();


        public static Contract GetByID(int? id)
        {
            return repo.Find(id);
        }
        public static List<Contract> GetContractByEmployee(int? id)
        {
            return repo.GetContractsWithEmployee(id);
        }
        public static bool AddContract(Contract contract)
        {
            return repo.Add(contract);
        }
        public static List<Contract> GetAllContracts()
        {
            return repo.All();
        }
        public static EditEmployeeContractDTO GetEditContract(int? id)
        {
            return EditEmployeeContractDTO.ToDTO(repo.Find(id));
        }
    }
}