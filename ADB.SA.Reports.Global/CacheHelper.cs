using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace ADB.SA.Reports.Global
{
    public class CacheHelper
    {
        /// <summary>
        /// Adds an item to cache if the item does not exists.
        /// </summary>
        /// <param name="manager">The instance of the cache manager.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The main object.</param>
        public static void AddToCacheWithCheck(ICacheManager manager,
                                                    string key,
                                                    object value)
        {
            AddToCache(manager, key, value);
        }

        public static void AddToCacheForOneMinute(ICacheManager manager,
                                                string key,
                                                object value)
        {
            manager.Add(key, value, CacheItemPriority.High, null,
                new AbsoluteTime(TimeSpan.FromMinutes(1)));
        }

        /// <summary>
        /// Adds an item to cache if the item does not exists.
        /// </summary>
        /// <param name="manager">The instance of the cache manager.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The main object.</param>
        private static void AddToCache(ICacheManager manager, string key, object value)
        {
            if (!manager.Contains(key))
            {
                manager.Add(key, value);
            }
        }

        public static void AddToCachePermanently(ICacheManager manager, string key, object value)
        { 
            manager.Add(key, value, CacheItemPriority.NotRemovable, null,
                new NeverExpired());
               
        }

        /// <summary>
        /// Adds an item to cache if the item not exist.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The main object.</param>
        public static void AddToCacheWithCheck(string key, object value)
        {
            ICacheManager manager = CacheFactory.GetCacheManager();
            AddToCache(manager, key, value);
        }

        /// <summary>
        /// Gets item from the cache if only the key exists.
        /// </summary>
        /// <typeparam name="T">The data type.</typeparam>
        /// <param name="manager">The instance of the cache manager.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>Returns the defined type T (typecasted).</returns>
        public static T GetFromCacheWithCheck<T>(ICacheManager manager, string key)
        {
            return GetFromCache<T>(key, manager);
        }

        /// <summary>
        /// Gets item from the cache if only the key exists.
        /// </summary>
        /// <typeparam name="T">The data type.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <returns>Returns the defined type T (typecasted).</returns>
        public static T GetFromCacheWithCheck<T>(string key)
        {
            ICacheManager manager = CacheFactory.GetCacheManager();
            return GetFromCache<T>(key, manager);
        }

        /// <summary>
        /// Gets item from the cache if only the key exists.
        /// </summary>
        /// <typeparam name="T">The data type.</typeparam>
        /// <param name="manager">The instance of the cache manager.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>Returns the defined type T (typecasted).</returns>
        private static T GetFromCache<T>(string key, ICacheManager manager)
        {
            if (manager.Contains(key))
            {
                return (T)manager.GetData(key);
            }
            return default(T);
        }

        /// <summary>
        /// Clears the items in the cache.
        /// </summary>
        public static void ClearCache()
        {
            ICacheManager manager = CacheFactory.GetCacheManager();
            manager.Flush();
        }
    }
}
