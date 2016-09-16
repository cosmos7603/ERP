namespace Corpnet.Profiling
{
    public interface IProfilerProvider
    {
        MiniProfiler Start();
        void Stop(bool discardResults);
        MiniProfiler GetCurrentProfiler();
    }
}
