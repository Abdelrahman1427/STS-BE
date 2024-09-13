
using Microsoft.AspNetCore.Http;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IService;
using PathFinder.Core.Interfaces.Shared.Repository;
using PathFinder.SharedKernel.Enums;

namespace PathFinder.BusinessLogic.Services.Shared
{
    public class RoleClaimService : IRoleClaimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;
        public RoleClaimService(IUnitOfWork unitOfWork, IHttpContextAccessor context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<List<RoleClaim>> GetRoleClaim(int roleId, Platform platform)
        {
            RolePermissionPlatform rolePermissionPlatform =
                platform == Platform.Website ? RolePermissionPlatform.Backend : RolePermissionPlatform.Mobile;

            var roleClaim = await _unitOfWork.GetRepositoryAsync<RoleClaim>()
                .GetListAsync(a => a.Role.Id == roleId &&
                                (a.PermissionPlatform == RolePermissionPlatform.Shared ||
                                 a.PermissionPlatform == rolePermissionPlatform));

            return roleClaim.ToList();
        }
    }
}
