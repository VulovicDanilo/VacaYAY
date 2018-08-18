using System.ComponentModel.DataAnnotations;

namespace VacaYAY.ViewModels
{
    public class CollectiveEmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
    }
}