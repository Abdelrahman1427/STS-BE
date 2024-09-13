using PathFinder.DataTransferObjects.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.NGO
{
    public class GetLookUpNgoDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ModelValidationResources), ErrorMessageResourceName = "EngNameEmpty")]
        public string Name { get; set; }
  
    }
}
