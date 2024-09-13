using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.CourseTransaction
{
    public class BeneficiarieDTO
    {
        public int? Id { get; set; }
        public string? FullName { get; set; } 
        public string? NickName { get; set; } 
        public string? IDNumber { get; set; } 
        public DateTime? Birthdate { get; set; } 
        public int? Age { get; set; } 
        public string? PhoneNumber { get; set; }
        public int? GenderId { get; set; } 
        public int? CityId { get; set; } 
        public int? BeneficiarieGovernorateId { get; set; }
        public int? EducationLevelId { get; set; } 
        public string? Workplace { get; set; }
        public string? Position { get; set; } 
        public bool? IsDisability { get; set; }
        public int? CourseTransactionID { get; set; }
        public int? DisabilityId { get; set; } // INT


    }
}
