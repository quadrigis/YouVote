using System.Web;

namespace YouVote.Models.Plugin.Ioc
{
    public class NewSessionState : ISessionState
    {
        private readonly HttpSessionStateBase _session;

        public NewSessionState(HttpSessionStateBase session)
        {
            _session = session;
        }

        public void Clear()
        {
            _session.RemoveAll();
        }

        public void Delete(string key)
        {
            _session.Remove(key);
        }

        public object Get(string key)
        {
            return _session[key];
        }

        public void Store(string key, object value)
        {
            _session[key] = value;
        }
    }

    public static class SessionExtensions
    {
        public static T Get<T>(this ISessionState sessionState, string key) where T : class
        {
            return sessionState.Get(key) as T;
        }
    }
}