using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Corpnet.Profiling.Helpers
{
	public class Utils
	{
		public static string IsNull(string s, string v)
		{
			if (String.IsNullOrEmpty(s))
				return v;
			else
				return s;
		}
	}
}
