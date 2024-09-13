using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Interventiom
{
    public class InterventionDTO : GetLookUpDefinitionDTO
    {
        public  int ObjectiveId { get; set; }
        [StringLength(60, ErrorMessage = "Code cannot exceed 60 characters.")]
        public string Code { get; set; } // Required, max length 60
    }
}