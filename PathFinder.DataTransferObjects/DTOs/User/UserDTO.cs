using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.DataTransferObjects.DTOs.User
{
    public class UserDTO : GetLookUpDefinitionDTO
    {
        public int  RoleId { get; set; }
        public  int  UserTypeId { get; set; }
    }
}
