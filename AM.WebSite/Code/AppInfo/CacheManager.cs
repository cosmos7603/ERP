using System;
using System.Runtime.Caching;

namespace AM.WebSite
{
	public static class CacheManager
	{
		#region Members
		private static MemoryCache _cache = MemoryCache.Default;
		#endregion

		#region Cache Management
		public static void Set(string key, object value)
		{
			// Item is not in cache... then create it
			var cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddDays(1) };

			// Set item in cache with key, and trigger action()
			_cache.Set(key, value, cacheItemPolicy);
		}

		public static object Get(string key)
		{
			// If the item is in the cache, and the user didn't request to refresh it
			if (_cache.Contains(key))
				return _cache.Get(key);

			return null;
		}
		#endregion
	}
}