namespace STS.DataTransferObjects.DTOs.Shared.Request
{
    public class PagingDTO<T> : PagingDTO
    {
        public T Filter { get; set; }
    }
    public class PagingDTO
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public SortingDTO? SortingDTO { get; set; }
    }
}
