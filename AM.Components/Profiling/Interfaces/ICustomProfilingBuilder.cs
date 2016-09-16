using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Corpnet.Profiling.Interfaces
{
	public interface ICustomProfilingBuilder
	{
		void BuildProfiling(MiniProfiler profiler);
	}
}
