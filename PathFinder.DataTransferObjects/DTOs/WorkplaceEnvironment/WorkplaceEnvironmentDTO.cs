using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.WorkplaceEnvironment
{
    public class WorkplaceEnvironmentDTO : GetLookUpDefinitionDTO
    {
        public  int  PrivateSectorId { get; set; }
    }
}
