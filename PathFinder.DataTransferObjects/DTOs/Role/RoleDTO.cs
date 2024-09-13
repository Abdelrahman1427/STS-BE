
using PathFinder.DataTransferObjects.DTOs.Shared.Request;

namespace PathFinder.DataTransferObjects.DTOs.Role
{
    public class RoleDTO : GetLookUpDefinitionDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
