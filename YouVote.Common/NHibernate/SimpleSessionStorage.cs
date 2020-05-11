using System.Collections.Generic;
using NHibernate;

namespace YouVote.Common.NHibernate
{
    public class SimpleSessionStorage : ISessionStorage
    {
        private readonly Dictionary<string, ISession> _sessionStorage = new Dictionary<string, ISession>();

        private readonly Dictionary<string, IStatelessSession> _statelessSessionsStorage = new Dictionary<string, IStatelessSession>();

        public IEnumerable<ISession> GetAllSessions()
        {
            return _sessionStorage.Values;
        }

        public IEnumerable<IStatelessSession> GetAllStatelessSessions()
        {
            return _statelessSessionsStorage.Values;
        }

        public ISession GetSessionForKey(string factoryKey)
        {
            ISession session;

            return _sessionStorage.TryGetValue(factoryKey, out session) ? session : null;
        }

        public IStatelessSession GetStatelessSessionForKey(string factoryKey)
        {
            IStatelessSession session;

            return _statelessSessionsStorage.TryGetValue(factoryKey, out session) ? session : null;
        }

        public void SetSessionForKey(string factoryKey, ISession session)
        {
            _sessionStorage[factoryKey] = session;
        }

        public void SetStatelessSessionForKey(string factoryKey, IStatelessSession session)
        {
            _statelessSessionsStorage[factoryKey] = session;
        }
    }
}