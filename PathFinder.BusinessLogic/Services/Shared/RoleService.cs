using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IService;
using PathFinder.Core.Interfaces.Shared.Repository;


namespace PathFinder.BusinessLogic.Services.Shared
{
    public class RoleService : CrudWithPaginateService<Role> ,IRoleService
    {
        private IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _unitOfWork.GetRepositoryAsync<Role>().FirstOrDefaultAsync(predicate: a => a.Name == roleName);
        }
    }
}
