using System;
using System.Collections.Generic;
using System.Web;

namespace Autoskola.Infrastructure.Cookie
{
    public static class CookieUtil
    {
        public static string GetCookieValue(string cookieName)
        {
            string cookieVal = String.Empty;
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (httpCookie != null)
                cookieVal = httpCookie.Value;
            return cookieVal;
        }

        public static void CreateCookie(string cookieName, string value, int? expirationDays)
        {
            var Cookie = new HttpCookie(cookieName, value);
            if (expirationDays.HasValue)
                Cookie.Expires = DateTime.Now.AddDays(expirationDays.Value);
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        public static void CreateCookie(string cookieName, string value, long? expirationMilisecund)
        {
            var Cookie = new HttpCookie(cookieName, value);
            if (expirationMilisecund.HasValue)
                Cookie.Expires = DateTime.Now.AddMilliseconds(expirationMilisecund.Value);
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        public static void DeleteCookie(string cookieName)
        {
            var Cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (Cookie != null)
            {
                Cookie.Expires = DateTime.Now.AddDays(-2);
                HttpContext.Current.Response.Cookies.Add(Cookie);
            }
        }

        public static bool CookieExists(string cookieName)
        {
            bool exists = false;
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
                exists = true;
            return exists;
        }

        public static Dictionary<string, string> GetAllCookies()
        {
            var cookies = new Dictionary<string, string>();
            foreach (string key in HttpContext.Current.Request.Cookies.AllKeys)
            {
                var httpCookie = HttpContext.Current.Request.Cookies[key];
                if (httpCookie != null)
                    cookies.Add(key, httpCookie.Value);
            }
            return cookies;
        }

        public static void DeleteAllCookies()
        {
            HttpCookieCollection x = HttpContext.Current.Request.Cookies;
            foreach (HttpCookie cook in x)
            {
                DeleteCookie(cook.Name);
            }
        }

        public static bool IsCookieEnabled()
        {
            CreateCookie("testCookie", "test", 1);
            if (CookieExists("testCookie"))
            {
                DeleteCookie("testCookie");
                return true;
            }
            return false;
        }
    }
}
