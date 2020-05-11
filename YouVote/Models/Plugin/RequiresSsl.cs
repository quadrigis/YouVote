using System;
using System.Web;
using System.Web.Mvc;

namespace  YouVote.Models.Plugin
{
    public class RequiresSsl : RequireHttpsAttribute
    {
        public bool RequireSecure = false;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (RequireSecure)
            {
                // default RequireHttps functionality
                base.OnAuthorization(filterContext);
            }
            else
            {
                // non secure requested
                if (filterContext.HttpContext.Request.IsSecureConnection)
                {
                    HandleNonHttpRequest(filterContext);
                }
            }
        }

        protected virtual void HandleNonHttpRequest(AuthorizationContext filterContext)
        {
            if (String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                // redirect to HTTP version of page
                string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}