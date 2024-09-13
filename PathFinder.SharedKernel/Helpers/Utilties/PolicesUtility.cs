using PathFinder.SharedKernel.Constants;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PathFinder.SharedKernel.Helpers.Utilties
{
    public static class Polices
    {
        public static void AddPolices(AuthorizationOptions authorizationOptions)
        {

            #region Employee

            authorizationOptions.AddPolicy(PolicyConstants.CanViewDefinitionsPolicy, policy =>
                 policy.RequireAssertion(context =>
                   context.User.HasClaim(c => c.Type == ClaimConstants.ShowEmployee && c.Value == "true") &&
                   context.User.HasClaim(c => c.Type == ClaimConstants.ViewEmployeesList && c.Value != "none")));

            authorizationOptions.AddPolicy(PolicyConstants.CanResetEmployeePasswordPolicy, policy =>
                 policy.RequireAssertion(context =>
                   context.User.HasClaim(c => c.Type == ClaimConstants.ShowEmployee && c.Value == "true") &&
                   context.User.HasClaim(c => c.Type == ClaimConstants.ResetPasswordForEmployee && c.Value != "none")));



            authorizationOptions.AddPolicy(PolicyConstants.CanUpdateDefinitionsPolicy, policy =>
                 policy.RequireAssertion(context =>
                   context.User.HasClaim(c => c.Type == ClaimConstants.ShowEmployee && c.Value == "true") &&
                   context.User.HasClaim(c => c.Type == ClaimConstants.UpdateEmployeeData && c.Value == "true")));


            authorizationOptions.AddPolicy(PolicyConstants.CanViewDefinitionsPolicy, policy =>
                policy.RequireAssertion(context =>
                  context.User.HasClaim(c => c.Type == ClaimConstants.ShowEmployee && c.Value == "true")));

            #endregion


        }


        private static bool IsNotAdmin(Claim c) => c.Type == ClaimConstants.Role && c.Value != AppConstants.Admin;
        private static bool IsAdminUser(Claim c) => c.Type == ClaimConstants.Role && c.Value == AppConstants.Admin;
    }
}


