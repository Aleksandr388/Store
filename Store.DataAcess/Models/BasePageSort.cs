namespace Store.DataAcess.Models
{
    public class BasePageSort
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool IsAccesing { get; set; }
        public string SortOrder { get; set; }
    }
}
