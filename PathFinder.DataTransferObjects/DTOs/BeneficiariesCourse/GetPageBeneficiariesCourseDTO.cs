using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.BeneficiariesCourse
{
    public class GetPageBeneficiariesCourseDTO
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? TrainerName { get; set; }
        public int? TrainingDays { get; set; }
    }
}
