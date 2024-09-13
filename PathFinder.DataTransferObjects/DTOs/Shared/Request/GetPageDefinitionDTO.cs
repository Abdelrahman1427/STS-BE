
namespace STS.DataTransferObjects.DTOs.Shared.Request
{
    public class GetPageDefinitionDTO
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
