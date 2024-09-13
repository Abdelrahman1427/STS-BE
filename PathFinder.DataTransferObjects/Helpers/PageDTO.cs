
namespace PathFinder.DataTransferObjects.Helpers
{
    public class PageDTO<T> : PageDTO
    {
        public T Filter { get; set; }
    }
    public class PageDTO
    { 
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 25;
        public string? OrderProp { get; set; }
        public bool IsOrderAsc { get; set; } = true;
        public string? TimeZone { get; set; }
    }
}
