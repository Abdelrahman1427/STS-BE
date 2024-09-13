using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Objectives
{
    public class ObjectivesDTO : GetLookUpDefinitionDTO
    {
        [StringLength(60, ErrorMessage = "Code cannot exceed 60 characters.")]
        public string Code { get; set; } // Required, max length 60
    }
}
