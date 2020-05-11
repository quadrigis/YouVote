using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Logging;
using YouVote.Common;
using YouVote.Common.Exceptions;
using YouVote.Models.Plugin.DataTables;
using YouVote.Models.Plugin.Ioc;

namespace YouVote
{
    public class MvcApplication : HttpApplication
    {
        private readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }

        private void InitAutomapper()
        {
            var automapperInstaller = new AutomapperInstaller();
            automapperInstaller.Init();
        }

        protected void Application_Start()
        {
            _logger.Info("Start");
            RegisterViewEngines();

            AreaRegistration.RegisterAllAreas();

            //BundleTable.Bundles.RegisterTemplateBundles();
            //BundleTable.Bundles.Add(new CssAppBundle.CssAppBundle1());
            //BundleTable.Bundles.Add(new CssAppBundle.JsAppBundle1());

            InitAutomapper();
            TypeResolver.Install();
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            //ValueProviderFactories.Factories.Add(new JsonDotNetValueProviderFactory());

            RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(DataTablesRequest), new DataTablesModelBinder());
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                var ci = (CultureInfo)Session["Culture"];
                if (ci == null)
                {
                    var langName = "en";
                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    }
                    ci = new CultureInfo(langName);
                    Session["Culture"] = ci;
                }
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }

        private void RegisterViewEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            _logger.Error(string.Empty, ex);
            var msg = "Sorry Error";
            var clear = false;

            if (ex.InnerException is SqlException)
            {
                var exInner = ex.InnerException as SqlException;
                if (exInner.Number == 547 && exInner.Message.Contains("The DELETE"))
                {
                    msg = Resources.Models.ValidRes.ForeginKeyError;
                    clear = true;
                }
            }

            if (ex is UiException)
            {
                clear = true;
                msg = ex.Message;
            }

            if(clear)
            {
                msg = msg.Replace('ę', 'e').Replace('ą', 'a').Replace('ś', 's').Replace('ć', 'c').Replace('ż', 'z')
                    .Replace('ź', 'z').Replace('ł', 'l').Replace('ó', 'o');
                var ctx = HttpContext.Current;
                ctx.Response.StatusCode = 666;
                ctx.Response.StatusDescription = msg;
                Server.ClearError();
            }
        }
    }
}