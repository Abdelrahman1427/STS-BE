
using PathFinder.Core.Entities;

namespace PathFinder.Core.Interface.IService
{
    public interface IUserService
    {
        Task<User> GetUserByNameOrEmailOrMobile(string userNameOrEmailOrMobile);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int userId);
    }
}
