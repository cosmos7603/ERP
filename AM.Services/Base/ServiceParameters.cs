using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.Services.Grid;

namespace AM.Services
{
    #region Pager
    public class PagerParameters
    {
		// Input
		public int PageSize { get; set; }
		public int PageIndex { get; set; }
		public string SortField { get; set; }
		public string SortDirection { get; set; }
		public string SearchVal { get; set; }

		public List<Filter> filters { get; set; }

		// Output
		public int RowCount { get; set; }
		public bool PartialResults { get; set; }

		// Calculated
		public int PageCount
		{
			get { return (PageSize > 0) ? (RowCount / PageSize) + 1 : 0; }
		}

		public int FirstRow
		{
			get { return (PageIndex * PageSize) + 1; }
		}

		public int LastRow
		{
			get { return ((PageIndex + 1) * PageSize) > RowCount ? RowCount : ((PageIndex + 1) * PageSize); }
		}
	}
	#endregion
}
