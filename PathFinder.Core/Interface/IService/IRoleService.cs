using PathFinder.Core.Entities;
using PathFinder.Core.Interface.Shared.IServices;

namespace PathFinder.Core.Interface.IService
{
    public interface IRoleService : ICrudWithPaginateService<Role>
    {
        Task<Role> GetRoleByName(string roleName);
    }
}
