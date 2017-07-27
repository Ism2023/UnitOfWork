namespace Pattern.Models.Request
{
    public class PagedItemRequestModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public FilterItem Filter { get; set; }
        public SortItem Sort { get; set; }

        public class FilterItem
        {
            public string field { get; set; }
            public string criteria { get; set; }
        }

        public class SortItem
        {
            public string field { get; set; }
            public bool useAscending { get; set; }
        }
    }
}
