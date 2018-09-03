using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacaYAY.Data.Repos;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Business
{
    public class ResolutionService
    {
        private static ResolutionRepository repo=new ResolutionRepository();
        private static RequestRepository reqRepo = new RequestRepository();


        public static bool Add(Resolution resolution)
        {
            return repo.Add(resolution);
        }
        public static bool Update(Resolution resolution)
        {
            return repo.Update(resolution);
        }
        
        public static List<Resolution> GetAllResolutions()
        {
            return repo.All();
        }
        public static List<Resolution> GetEmployeesResolutions(int? EmployeeID)
        {
            return repo.GetEmployeesResolutions(EmployeeID);
        }
        public static List<Resolution> GetThisYearsResolutions(int? EmployeeID,int year)
        {
            return repo.GetEmployeesResolutions(EmployeeID)
                .Where(x => x.ApprovalDate.Year == year).ToList();
        }
    }
}