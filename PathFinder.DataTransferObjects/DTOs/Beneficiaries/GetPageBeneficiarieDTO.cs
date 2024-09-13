using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.CourseTransaction
{
    public class GetPageBeneficiarieDTO
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public string? Position { get; set; }


    }
}
