using System;
using System.Configuration;
using System.Web;
using YouVote.Common.NHibernate;
using YouVote.Model;

namespace YouVote.Services
{
    public class Global : HttpApplication
    {
        private ISessionStorage _webSessionStorage;

        private static string GetCurrentUser()
        {
            try
            {
                return HttpContext.Current.User.Identity.Name.Trim();
            }
            catch (NullReferenceException)
            {
                return "N/A";
            }
        }

        private void InitializeNHibernateSession()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["FirstConnectionString"].ConnectionString;

            NhConfig.InitNhibernateSession(_webSessionStorage, GetCurrentUser, HttpContext.Current.IsDebuggingEnabled,
                new[] { Server.MapPath("~/bin/YouVote.Model.dll"), Server.MapPath("~/nhibernate.config") }, connectionString);
        }

        public override void Init()
        {
            _webSessionStorage = new WebSessionStorage(this);
            base.Init();
            NHibernateInitializer.Instance().InitializeNHibernateOnce(InitializeNHibernateSession);
        }
    }
}