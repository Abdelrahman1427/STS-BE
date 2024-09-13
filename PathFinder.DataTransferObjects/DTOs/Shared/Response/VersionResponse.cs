
namespace STS.DataTransferObjects.DTOs.Shared.Response
{
    public class VersionResponse
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Log { get; set; }
        public string Platform { get; set; }
        public bool IsMandatory { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
