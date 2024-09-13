
namespace PathFinder.DataTransferObjects.DTOs.Shared.Response
{
    public class ResultResponse
    {
        public bool Status { get; set; }
    }

    public class ResultDTO<T> where T : class
    {
        public T Value { get; set; }
    }
}
