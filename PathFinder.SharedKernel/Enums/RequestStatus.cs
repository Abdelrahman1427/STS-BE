using STS.DataTransferObjects.Resources;
using System.ComponentModel.DataAnnotations;

namespace STS.SharedKernel.Enums
{
    public enum RequestStatus
    {
        [Display(ResourceType = typeof(CoreResources), Name = "Pending")]
        Pending =1,
        [Display(ResourceType = typeof(CoreResources), Name = "Accepted")]
        Accepted,
        [Display(ResourceType = typeof(CoreResources), Name = "Declined")]
        Declined,
        [Display(ResourceType = typeof(CoreResources), Name = "Canceled")]
        Canceled,
        [Display(ResourceType = typeof(CoreResources), Name = "Delete")]
        Delete,
        [Display(ResourceType = typeof(CoreResources), Name = "CarryOver")]
        CarryOver,
        [Display(ResourceType = typeof(CoreResources), Name = "AdjustBalance")]
        AdjustBalance,
        [Display(ResourceType = typeof(CoreResources), Name = "AcceptedNoActionTaken")]
        AcceptedNoActionTaken,
        [Display(ResourceType = typeof(CoreResources), Name = "DeclinedNoActionTaken")]
        DeclinedNoActionTaken,
        [Display(ResourceType = typeof(CoreResources), Name = "AcceptedSkippedApprovals")]
        AcceptedSkippedApprovals
    }
}
