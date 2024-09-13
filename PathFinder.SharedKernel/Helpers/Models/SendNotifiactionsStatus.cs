

namespace PathFinder.Common.Helpers.Models
{
    public class SendNotifiactionsStatus
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }
    public class NotifiactionsResult
    {
        public List<SendNotifiactionsStatus> notifiactionsStatuses { get; set; } = new List<SendNotifiactionsStatus>();
        public bool FinalStatus { get; set; }
    }
}
