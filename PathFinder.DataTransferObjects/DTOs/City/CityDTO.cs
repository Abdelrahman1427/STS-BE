using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.City
{
    public class CityDTO : GetLookUpDefinitionDTO
    {
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "Required")]
        public int GovernorateId { get; set; }

    }
}
