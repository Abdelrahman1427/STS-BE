using System.Security.Claims;

namespace PathFinder.SharedKernel.Polices
{
    public interface IPolicyUtils
    {
        Task<bool> HasPolicy(ClaimsPrincipal user, string policyName);
    }
}
