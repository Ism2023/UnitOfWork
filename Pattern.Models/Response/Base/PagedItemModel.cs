using System.Collections.Generic;

namespace Pattern.Models.Response.Base
{
    public class PagedItemModel<T>
    {
        public enum SortOrderEnum { ASC, DESC }
        public int CurrentPage { get; set; } = 1;
        public bool BoF { get; set; }
        public bool EoF { get; set; }
        public int PageSize { get; set; }
        public int StartPosition { get; set; } = 1;
        public int EndPosition { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
