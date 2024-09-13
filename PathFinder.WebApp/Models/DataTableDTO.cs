namespace PathFinder.WebApp.Models
{
    public class DataTableDTO<T> where T : class
    {
        public int Draw { get; set; }
        public IList<T> Data { get; set; }
        public int RecordsFiltered { get; set; }
        public int recordsTotal { get; set; }
    }
}
