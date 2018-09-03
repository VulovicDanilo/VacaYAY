using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VacaYAY.Common;
using VacaYAY.Entities.Contracts;
using VacaYAY.Entities.Employees;
using VacaYAY.Entities.ExtraDays;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Business.DTOs
{
    public class RegularReportDTO
    {
        public string SerialNumber { get; set; }
        public string BodyContent { get; set; }
        public string FinishingContent { get; set; }

        public RegularReportDTO(Employee employee, Request request, Resolution resolution)
        {
            SerialNumber = resolution.SerialNumber;
            GenerateBodyContent(employee, request, resolution);
            GenerateFinishingContent(employee, request, resolution);
        }
        private void GenerateBodyContent(Employee employee, Request request, Resolution resolution)
        {
            StringBuilder s = new StringBuilder();
            List<Resolution> resolutions = ResolutionService.GetThisYearsResolutions(employee.EmployeeID, DateTime.Now.Year);

            string nowYear = DateTime.Now.Year.ToString();
            string previousYear = (DateTime.Now.Year - 1).ToString();
            string EmployeeName = employee.Name;
            string EmployeeLastname = employee.LastName;
            string EmployeeCity = employee.City;
            string EmployeeProfession = employee.Profession;
            bool hasPreviousResolutions = resolutions.Count > 1;
            Resolution firstResolution = resolutions.OrderBy(x => x.ApprovalDate).First();
            int i = 1;

            // 1. Basic part
            s.Append($"{i++}. Zaposlenom, {EmployeeName} {EmployeeLastname} iz {EmployeeCity}, na poslovima {EmployeeProfession}" +
                $", odobreno je korišćenje godišnjeg odmora za {nowYear}. godinu u trajanju od {employee.DefaultVacationDays} radna dana");
            if (hasPreviousResolutions)
            {
                s.Append($" rešenjem o korišćenju godišnjeg odmora br. {firstResolution.SerialNumber} od {firstResolution.ApprovalDate.ToString("dd.MM.yyyy.")} godine");
            }
            s.AppendLine(".").AppendLine();

            // Leftover part
            int leftover = resolutions.Sum(x => x.LeftoverUsed) + employee.LeftoverVacationDays;
            if (leftover > 0)
            {
                s.Append($"{i++}. Zaposleni ima preostali godišnji odmor u trajanju od {leftover} radna dana godišnjeg odmora iz {previousYear}. godine.");
                s.AppendLine().AppendLine();
                s.Append($"Neiskorišćen godišnji odmor iz predhodne godine zaposleni može iskoristiti do 30.juna {nowYear}. godine.");
            }
            // Previous Resolutions part
            if (hasPreviousResolutions)
            {
                int total = resolutions.Sum(x => x.Request.NumberOfDays);
                s.Append($"{i++}. Zaposleni je iskoristio 15 radna dana godišnjeg odmora na sledeći način:").AppendLine().AppendLine();
                foreach (var item in resolutions)
                {
                    s.Append($"- {item.Request.NumberOfDays} radna dana (");
                    if (item.LeftoverUsed > 0)
                    {
                        s.Append($"{item.LeftoverUsed} dana preostalog godišnjeg odmora iz {previousYear}. godine i ");
                    }
                    s.Append($"{item.RegularUsed} radnih dana godišnjeg odmora za {nowYear}. godinu) ");
                    s.Append($"u skladu sa Rešenjem o korišćenju godišnjeg odmora br. {item.SerialNumber} od {item.ApprovalDate.ToString("dd.MM.yyyy.")} godine.").AppendLine().AppendLine();
                }
            }

            // Current Resolution
            s.Append($"{i++}. Zaposleni će koristiti godišnji odmor u trajanju od {resolution.Request.NumberOfDays} radna dana (");
            if (resolution.LeftoverUsed > 0)
            {
                s.Append($"{resolution.LeftoverUsed} dana preostalog godišnjeg odmora iz {previousYear}. godine i ");
            }
            s.Append($"{resolution.RegularUsed} radna dana godišnjeg odmora za {nowYear}. godinu) u periodu od {resolution.Request.StartDate.ToString("dd.MM.yyyy.")} godine do {resolution.Request.EndDate.ToString("dd.MM.yyyy.")} godine.");
            s.AppendLine().AppendLine();

            // Boring part

            s.Append($"{i++}. Zaposleni će koristiti preostali godišnji odmor za {nowYear}. godinu u trajanju od 8 radnih dana u jednom ili više delova, u skladu sa dogovorom sa Poslodavcem, Pravilnikom o radu i Ugovorom o radu, o čemu će biti doneto posebno rešenje.");
            s.AppendLine().AppendLine();

            s.Append($"{i++}. Za vreme korišćenja godišnjeg odmora zaposlenom pripada naknada zarade u visini  isplaćene zarade zaposlenom za prethodni mesec, odnosno u visini prosečne zarade u prethodnih 12 meseci, ukoliko je to za zaposlenog povoljnije, u skladu sa Pravilnikom o radu.");
            s.Append($"{i++}. Rešenje o korišćenju godišnjeg odmora je punovažno bez pečata i potpisa.");


            // Over
            BodyContent = s.ToString();
        }
        private void GenerateFinishingContent(Employee employee, Request request, Resolution resolution)
        {
            StringBuilder s = new StringBuilder();

            if (employee.Contracts.Count > 0)
            {
                Contract contract = employee.Contracts.Last();
                if (contract.EndDate.HasValue)
                {
                    s.Append($"Zaposleni je dana {contract.StartDate.ToString("dd.MM.yyyy.")}. godine zasnovao radni odnos do {contract.EndDate.Value.ToString("dd.MM.yyyy.")}. godine Ugovorom o radu br. {contract.SerialNumber} od 31.10.2017. godine.");
                }
                else
                {
                    s.Append($"Zaposleni je dana {contract.StartDate.ToString("dd.MM.yyyy.")}. godine zasnovao radni odnos na neodređeno vreme Ugovorom o radu br. {contract.SerialNumber} od {contract.StartDate.ToString("dd.MM.yyyy.")} godine.");
                }
                s.AppendLine().AppendLine();
            }
            s.Append($"Na osnovu kriterijuma utvrđenih Zakonom i Pravilnikom o radu Poslodavca, određena je dužina godišnjeg odmora uzimajući kao parameter zakonski minimum u trajanju od 20 radnih dana. ");
            List<ExtraDays> list = ExtraDaysService.GetEmployeesExtraDays(employee.EmployeeID);
            if (list.Count > 0)
            {
                s.Append("Zakonski minimum je uvećan po osnovu ");
                var last = list.Last();
                foreach (var item in list)
                {
                    s.Append($"{item.Basis.GetEnumDescription()} u trajanju od {item.Days} radna dana");
                    if (item.Equals(last))
                    {
                        s.AppendLine(".");
                    }
                    else
                    {
                        s.Append(",");
                    }
                }
                s.AppendLine();
            }
            s.Append("U zavisnosti od potrebe posla i na osnovu obavljene konsultacije zaposlenog," +
                " određeno je vreme korišćenja godišnjeg odmora kao u dispozitivu ovog rešenja.");
            s.AppendLine().AppendLine();
            s.Append("POUKA O PRAVNOM LEKU:").AppendLine().AppendLine();
            s.Append("Ovo rešenje je konačno i protiv istog zaposleni ima pravo da pokrene spor pred nadležnim sudom, u roku od 60 dana od dana dostavljanja ovog rešenja.").AppendLine().AppendLine();
            s.Append($"U Nišu, {resolution.ApprovalDate.ToString("dd.MM.yyyy.")}. godine").AppendLine().AppendLine();
            FinishingContent = s.ToString();
        }

    }
}