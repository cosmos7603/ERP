using System;
using System.Collections.Generic;

namespace Corpnet.Profiling.Interfaces
{
    public interface IStorage
    {
        void Save(MiniProfiler profiler);
    }
}
