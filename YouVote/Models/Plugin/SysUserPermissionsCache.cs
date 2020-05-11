using System;
using System.Web;
using System.Web.Caching;

namespace YouVote.Models.Plugin
{
    public class SysUserPermissionsCache
    {
        private static string GetCacheKey(string userName)
        {
            return string.Format("UserPermission_{0}", userName);
        }

        private static string GetCacheKey()
        {
            return GetCacheKey(HttpContext.Current.User.Identity.Name);
        }

        public static string[] GetPermissions(string userName)
        {
            var cacheKey = GetCacheKey(userName);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return (string[])HttpRuntime.Cache[cacheKey];
            }
            return null;
        }

        public static string[] GetPermissions()
        {
            return GetPermissions(HttpContext.Current.User.Identity.Name);
        }

        public static void ClearCache(string userName)
        {
            var cacheKey = GetCacheKey(userName);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                HttpRuntime.Cache.Remove(cacheKey);
            }
        }

        public static void ClearCache()
        {
            ClearCache(HttpContext.Current.User.Identity.Name);
        }

        public static void InsertToCache(string userName, string[] permissions, double cacheTimeoutInMinutes = 0)
        {
            var cacheKey = GetCacheKey(userName);
            HttpRuntime.Cache.Insert(cacheKey, permissions, null, DateTime.Now.AddMinutes(cacheTimeoutInMinutes), Cache.NoSlidingExpiration);
            
        }
    }
}