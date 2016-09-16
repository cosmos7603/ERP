using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Corpnet.Profiling.Storage
{
	[Serializable]
	public class BlockingQueue<T>
	{
		private readonly Queue<T> _queue = new Queue<T>();

		public void Enqueue(T item)
		{
			lock (_queue)
			{
				_queue.Enqueue(item);
			}
		}

		public bool Dequeue(out T item)
		{
			item = default(T);

			lock (_queue)
			{
				if (_queue.Count > 0)
				{
					item = _queue.Dequeue();
					return true;
				}
			}

			return false;
		}

		public long Count()
		{
			lock (_queue)
			{
				return _queue.Count;
			}
		}
	}
}
