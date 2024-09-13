using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PathFinder.SharedKernel.Polices
{
    public class PolicyUtils : IPolicyUtils
    {
        public readonly IAuthorizationService _authorizationService;

        public PolicyUtils(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<bool> HasPolicy(ClaimsPrincipal user, string policyName)
        {
            return (await _authorizationService.AuthorizeAsync(user, policyName)).Succeeded;
        }
    }
}
