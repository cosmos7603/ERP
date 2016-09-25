using System.Collections.Generic;
using System.Web.Helpers;

namespace AM.Services.Grid
{
	public class SortOptions
	{
		public List<string> PropertyChain { get; set; }
		public SortDirection Direction { get; set; }
	}
}
