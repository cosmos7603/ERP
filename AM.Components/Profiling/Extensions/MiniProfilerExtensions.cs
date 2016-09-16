using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Corpnet.Profiling
{
    public static class MiniProfilerExtensions
    {
        public static T Inline<T>(this MiniProfiler profiler, Func<T> selector, string name)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (profiler == null) return selector();
            using (profiler.StepImpl(name, ""))
            {
                return selector();
            }
        }

        public static IDisposable Step(this MiniProfiler profiler, string name, string sql)
        {
			return profiler == null ? null : profiler.StepImpl(name, sql);
        }

		public static IDisposable Step(this MiniProfiler profiler, string name)
		{
			return profiler == null ? null : profiler.StepImpl(name, "");
		}
    }
}