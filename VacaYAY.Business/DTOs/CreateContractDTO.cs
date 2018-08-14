using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VacaYAY.Entities.Contracts;
using System.Web.Hosting;

namespace VacaYAY.Business.DTOs
{

    public class CreateContractDTO
    {
        public string SerialNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public HttpPostedFileBase File { get; set; }

        public static Contract ToEntity(CreateContractDTO dto)
        {
            Contract contract = new Contract()
            {
                SerialNumber = dto.SerialNumber,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
            };
            return contract;
        }
        public static List<Contract> ToEntityList(List<CreateContractDTO> dtos,string userID)
        {
            List<Contract> list = new List<Contract>();
            foreach(var dto in dtos)
            {
                Contract contract = new Contract()
                {
                    SerialNumber = dto.SerialNumber,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                };
                var fileName = Path.GetFileName(dto.File.FileName);
                var filepath = "~/Contracts/" + userID + "/";
                var directory = HttpContext.Current.Server.MapPath(filepath);
                var path = Path.Combine(directory, fileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                else if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                dto.File.SaveAs(path);
                contract.Link = filepath + fileName;
                list.Add(contract);
            }
            return list;
        }
    }
}