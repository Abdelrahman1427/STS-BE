
namespace STS.DataTransferObjects.Helpers
{
    public class APIResult
    {
        public bool state { get; set; }
        public string? message { get; set; }
        public dynamic? entity { get; set; }
    }
}
