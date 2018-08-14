using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacaYAY.Business.DTOs
{
    public class ShowRequestDTO
    {
        public int RequestID { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime StartDate { get; set; }

    }
}
