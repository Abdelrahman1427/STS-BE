using PathFinder.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.SharedKernel.Enums
{
    public enum ResponseStatus
    {
        [Display(ResourceType = typeof(CoreResources), Name = "Ok")]
        Ok = 200,
        [Display(ResourceType = typeof(CoreResources), Name = "Created")]
        Created = 201,
        [Display(ResourceType = typeof(CoreResources), Name = "NoContent")]
        NoContent = 204,
        [Display(ResourceType = typeof(CoreResources), Name = "BadRequest")]
        BadRequest = 400,
        [Display(ResourceType = typeof(CoreResources), Name = "Unauthorized")]
        Unauthorized = 401,
        [Display(ResourceType = typeof(CoreResources), Name = "Forbidden")]
        Forbidden = 403,
        [Display(ResourceType = typeof(CoreResources), Name = "NotFound")]
        NotFound = 404,
        [Display(ResourceType = typeof(CoreResources), Name = "MethodNotAllowed")]
        MethodNotAllowed = 405,
        [Display(ResourceType = typeof(CoreResources), Name = "RequestTimeout")]
        RequestTimeout = 408,
        [Display(ResourceType = typeof(CoreResources), Name = "Conflict")]
        Conflict = 409,
        [Display(ResourceType = typeof(CoreResources), Name = "InternalServerError")]
        InternalServerError = 500,
        [Display(ResourceType = typeof(CoreResources), Name = "NotImplemented")]
        NotImplemented = 501,
        [Display(ResourceType = typeof(CoreResources), Name = "BadGateway")]
        BadGateway = 502,
        [Display(ResourceType = typeof(CoreResources), Name = "ServiceUnavailable")]
        ServiceUnavailable = 503,
        [Display(ResourceType = typeof(CoreResources), Name = "GatewayTimeout")]
        GatewayTimeout = 504
    }
}
