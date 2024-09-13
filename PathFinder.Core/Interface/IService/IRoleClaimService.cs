using PathFinder.Core.Entities;
using PathFinder.SharedKernel.Enums;

namespace PathFinder.Core.Interface.IService
{
    public interface IRoleClaimService
    {
        Task<List<RoleClaim>> GetRoleClaim(int roleId, Platform platform);
    }
}
