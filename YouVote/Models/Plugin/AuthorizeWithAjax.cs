using System.Web;
using System.Web.Mvc;

namespace YouVote.Models.Plugin
{
    /// <summary>
    /// http://stackoverflow.com/questions/2580596/how-do-you-handle-ajax-requests-when-user-is-not-authenticated
    /// </summary>
    public class AuthorizeWithAjax : AuthorizeAttribute
    {
        private class Http403Result : ActionResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                // Set the response code to 403.
                context.HttpContext.Response.StatusCode = 403;
                context.HttpContext.Response.StatusDescription = "Sorry. You don't have permissions. Please login.";
            }
        }

        private readonly bool _authorize;

        public AuthorizeWithAjax()
        {
            _authorize = true;
        }

        //OptionalAuthorize is turned on on base controller class, so it has to be turned off on some controller. 
        //That is why parameter is introduced.
        public AuthorizeWithAjax(bool authorize)
        {
            _authorize = authorize;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //When authorize parameter is set to false, not authorization should be performed.
            if (!_authorize)
            {
                return true;
            }

            //if (httpContext.Session != null && httpContext.Session.IsNewSession)
            //{
            //    return false;
            //}

            var result = base.AuthorizeCore(httpContext);

            return result;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                //Ajax request doesn't return to login page, it just returns 403 error.
                filterContext.Result = new Http403Result();
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}