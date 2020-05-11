using System;
using System.Web;
using System.Web.Optimization;

namespace YouVote.Models.Plugin
{
    class CssAppBundle
    {
        // http://www.beletsky.net/2012/04/new-in-aspnet-mvc4-bundling-and.html
        // http://odetocode.com/Blogs/scott/archive/2012/03/20/yet-another-bundling-approach-for-mvc-4.aspx


        public class CustomBundle : Bundle
        {
            public CustomBundle(string virtualPath)
                : base(virtualPath)
            {

            }

            public void AddFiles(params string[] files)
            {
                Include(files);
            }

            public void SetTransform<T>() where T : IBundleTransform
            {
                if (HttpContext.Current.IsDebuggingEnabled)
                {
                    Transforms.Clear();
                }
                else
                {
                    var t = Activator.CreateInstance<T>();
                    Transforms.Add(t);
                }
            }
        }

        public class CssBundle : CustomBundle
        {
            public CssBundle(string virtualPath)
                : base(virtualPath)
            {
                SetTransform<CssMinify>();
            }
        }

        public class JsBundle : CustomBundle
        {
            public JsBundle(string virtualPath)
                : base(virtualPath)
            {
                SetTransform<JsMinify>();
            }
        }


        public class CssAppBundle1 : CssBundle
        {
            public CssAppBundle1()
                : base("~/Content/b1")
            {
                AddFiles(
                    "~/Content/Site.css",
                    "~/Scripts/jquery-ui-1.10.3/ui/themes/base/jquery-ui.css",
                    "~/Scripts/bootstrap/css/bootstrap.min.css",
                    "~/Scripts/bootstrap/css/bootstrap-theme.min.css"
                    );
            }
        }

        public class JsAppBundle1 : JsBundle
        {
            public JsAppBundle1()
                : base("~/Scripts/s1")
            {
                AddFiles(
                    "~/Scripts/jquery-ui-1.10.3/jquery-1.9.1.js",
                    "~/Scripts/jquery-ui-1.10.3/ui/jquery-ui.js",
                    "~/Scripts/jquery.validate.min.js",
                    "~/Scripts/jquery.validate.unobtrusive.min.js",
                    "~/Scripts/modernizr-1.7.min.js",
                    "~/Scripts/bootstrap/js/bootstrap.min.js"
                    );
            }
        }
    }
}
