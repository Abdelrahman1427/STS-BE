namespace PathFinder.DataTransferObjects.DTOs.Shared.Request
{
    public class SortingDTO
    {
        public string? OrderBy { get; set; }
        public bool IsOrderAsc { get; set; } = true;
    }
}
