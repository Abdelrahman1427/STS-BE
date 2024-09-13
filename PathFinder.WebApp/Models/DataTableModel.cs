using PathFinder.DataTransferObjects.Helpers;

namespace PathFinder.WebApp.Models
{
    public class DataTableSearch
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Regex { get; set; }
    }
    public class DataTableModel<T>: DataTableModel
    {
        public T CustomData { get; set; }
    }
    public class DataTableModel
    {
        public int Draw { get; set; }
        public List<DataTableColumn> Columns { get; set; }
        public List<DataTableOrder> Order { get; set; }
        public int Length { get; set; }
        public int Start { get; set; }
        public DataTableSearch Search { get; set; }
        public List<DataTableSearch> SearchData { get; set; }
        public int PageNo => (int)Math.Round((double)(this.Start / this.Length));
        public DataTableDTO<T> GetDataTableModel<T>(Pagination<T> page) where T : class
        {
            return new DataTableDTO<T>()
            {
                Draw = Draw,
                Data = page.Items,
                recordsTotal = page.Count,
                RecordsFiltered = page.Count,
            };
        }
    }
}
