using System.Collections.Generic;

namespace WebErpExt5.Models
{
    public class PagedResult<TPoco>
    {
        readonly IEnumerable<TPoco> _items;
        readonly int _totalCount;

        public PagedResult(IEnumerable<TPoco> items, int totalCount)
        {
            _items = items;
            _totalCount = totalCount;
        }

        public IEnumerable<TPoco> Items { get { return _items; } }
        public int TotalCount { get { return _totalCount; } }
    }
}
