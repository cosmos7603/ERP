using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Corpnet.Profiling.HttpModule
{
	public class ResponseLengthStream : MemoryStream
	{
		private readonly Stream _responseStream;

		public ResponseLengthStream(Stream responseStream)
		{
			_responseStream = responseStream;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (MiniProfiler.Current != null)
				MiniProfiler.Current.ResponseSize += count;
			
			_responseStream.Write(buffer, offset, count);
		}
	}
}
