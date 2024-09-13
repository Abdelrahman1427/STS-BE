using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.CourseTransaction
{
    public class CourseTransactionDTO
    {
        public int? Id { get; set; }
        public string? Code { get; set; } 
        public string? TrainerName { get; set; } 
        public int? TrainingDays { get; set; } 
        public DateTime? CourseTransactionDate { get; set; } 
        public string? MELCoordinator { get; set; }
        public string? PlaceDescription { get; set; } 
        public string? Latitude { get; set; }
        public string? Longitude { get; set; } 
        public string? CDACoordinator { get; set; } 
        public int? InterventionId { get; set; }
        public int? GovernorateId { get; set; } 
        public int? NonGovermntalOrgniszationId { get; set; } 
        public int? PartnerId { get; set; } 
        public int? CommunityDevlopmentAssositionId { get; set; } 
        public int? PrivateSectorId { get; set; }
        public int? IndicatorTransactionId { get; set; }
        public int? UserTypeId { get; set; }
        public int? ObjectiveId { get; set; }


    }
}
