using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web;
using AM.DAL;
using AM.Services;

namespace AM.Services
{
	public static class CacheService
	{
		#region Members
		private static string _cacheName = "ServiceCache";
		private static MemoryCache _cache = new MemoryCache(_cacheName);
		#endregion

		#region Properties
		public static List<string> CachedItems
		{
			get
			{
				var itemList = new List<string>();

				foreach (var item in _cache)
					itemList.Add(item.Key);

				return itemList;
			}
		}
		#endregion

		#region Methods
		internal static object GetCachedItem(string key, Func<object> action)
		{
			return GetCachedItem(key, new List<object>() , action);
		}

		internal static object GetCachedItem(string key, int param, Func<object> action)
		{
			if (param <= 0)
				return null;

			return GetCachedItem(key, new List<object> { param } , action);
		}

		internal static object GetCachedItem(string key, string param, Func<object> action)
		{
			if (string.IsNullOrEmpty(param))
				return null;

			return GetCachedItem(key, new List<object> { param }, action);
		}

		internal static object GetCachedItem(string key, bool param, Func<object> action)
		{
			return GetCachedItem(key, new List<object> { param }, action);
		}

		internal static object GetCachedItem(string key, List<object> p, Func<object> action)
		{
			// Params
			if (p != null && p.Count > 0)
				key = key + "/" + String.Join("|", p);

			// If the item is in the cache, and the user didn't request to refresh it
			if (_cache.Contains(key))
				return _cache.Get(key);

			// Item is not in cache... then create it
			var cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddDays(1) };

			// Set item in cache with key, and trigger action()
			_cache.Set(key, action(), cacheItemPolicy);

			// Return cached object
			return _cache.Get(key);
		}

		internal static void ClearItem(string key)
		{
			if (_cache.Contains(key))
				_cache.Remove(key);
		}

		internal static void ClearAll()
		{
			_cache.Dispose();
			_cache = new MemoryCache(_cacheName);
		}
		#endregion
	}
}