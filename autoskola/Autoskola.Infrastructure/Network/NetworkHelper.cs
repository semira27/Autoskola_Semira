using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Autoskola.Infrastructure.Network
{
    public static class NetworkHelper
    {

        public static string GetIP(System.Web.HttpContext httpContext)
        {
            string ipAdresa;
            try
            {
                ipAdresa = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            catch (Exception)
            {
                ipAdresa = String.Empty;
            }

            if (!string.IsNullOrEmpty(ipAdresa))
            {
                string[] ipRange = ipAdresa.Split(',');
                int le = ipRange.Length - 1;
                string trueIp = ipRange[le];
            }
            else
            {
                try
                {
                    ipAdresa = httpContext.Request.ServerVariables["REMOTE_ADDR"];
                }
                catch (Exception)
                {
                    ipAdresa = String.Empty;
                }
            }

            return ipAdresa;
        }
        public static string GetUserAgent(HttpContext httpContext)
        {
            try
            {
                return httpContext.Request.UserAgent;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
    }
}
