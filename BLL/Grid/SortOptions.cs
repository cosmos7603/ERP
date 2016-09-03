using System.Collections.Generic;

namespace BLL.Grid
{
    public class SortOptions
    {
        public List<string> PropertyChain { get; set; }
        public OrderDirection Direction { get; set; }
    }
}
