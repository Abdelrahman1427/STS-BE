using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.Policy
{
    public class PolicyDTO : GetLookUpDefinitionDTO
    {
        public  int PSId { get; set; }
    }
}
