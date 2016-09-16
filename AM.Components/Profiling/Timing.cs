using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Corpnet.Profiling
{
	[Serializable]
    public class Timing : IDisposable
	{
		#region Members
		private readonly long _startTicks;
		#endregion

		#region Constructors
		public Timing()
        {
        }

        public Timing(MiniProfiler profiler, string name, string sql)
        {
            Id = Guid.NewGuid();
            Profiler = profiler;
            Name = name;
			SQL = sql;

            _startTicks = profiler.ElapsedTicks;
            StartMilliseconds = profiler.GetRoundedMilliseconds(_startTicks);
        }
		#endregion

		#region Properties
		internal MiniProfiler Profiler { get; private set; }
		public Guid Id { get; set; }
		public int DbTimingId { get; set; }
        public string Name { get; set; }
		public string SQL { get; set; }
        public decimal? DurationMilliseconds { get; set; }
        public decimal StartMilliseconds { get; set; }
		public long DataSize { get; set; }
		public int DataRowCount { get; set; }
		public List<Timing> Children { get; set; }
        public Timing ParentTiming { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
		#endregion

		#region Methods
		public override bool Equals(object other)
        {
            return other is Timing && Id.Equals(((Timing)other).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void Stop()
        {
			Stop(0, 0);
        }

		public void Stop(long dataSize, int dataRowCount)
		{
			if (DurationMilliseconds == null)
			{
				DurationMilliseconds = Profiler.GetDurationMilliseconds(_startTicks);
			}
			
			// Only save if assining
			if (dataSize > 0)
				this.DataSize = dataSize;

			if (dataRowCount > 0)
				this.DataRowCount = dataRowCount;
		}

        void IDisposable.Dispose()
        {
            Stop();
        }
		
        public void AddChild(Timing timing)
        {
            if (Children == null)
                Children = new List<Timing>();

            Children.Add(timing);
            timing.ParentTiming = this;
		}
		#endregion
	}
}