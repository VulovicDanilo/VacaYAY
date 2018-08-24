using VacaYAY.Entities.Employees;

namespace VacaYAY.Business.DTOs
{
    public class CollectiveEmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int RequestID { get; set; }

        public static CollectiveEmployeeDTO ToDTO(DetailsRequestDTO request)
        {
            CollectiveEmployeeDTO dto = new CollectiveEmployeeDTO()
            {
                EmployeeID = request.Employee.EmployeeID,
                Name = request.Employee.Name + " " + request.Employee.LastName,
                RequestID=request.RequestID,
            };
            return dto;
        }
    }
}