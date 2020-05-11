using System;
using System.Collections.Generic;
using NHibernate;

namespace YouVote.Common.NHibernate
{
    public class ThreadSessionStorage : ISessionStorage
    {
        [ThreadStatic]
        private static readonly SimpleSessionStorage SessionStorage = new SimpleSessionStorage();

        public IEnumerable<ISession> GetAllSessions()
        {
            var storage = GetSimpleSessionStorage();
            return storage.GetAllSessions();
        }

        public IEnumerable<IStatelessSession> GetAllStatelessSessions()
        {
            var storage = GetSimpleSessionStorage();
            return storage.GetAllStatelessSessions();
        }

        public ISession GetSessionForKey(string factoryKey)
        {
            var storage = GetSimpleSessionStorage();
            return storage.GetSessionForKey(factoryKey);
        }

        public IStatelessSession GetStatelessSessionForKey(string factoryKey)
        {
            var storage = GetSimpleSessionStorage();
            return storage.GetStatelessSessionForKey(factoryKey);
        }

        public void SetSessionForKey(string factoryKey, ISession session)
        {
            var storage = GetSimpleSessionStorage();
            storage.SetSessionForKey(factoryKey, session);
        }

        public void SetStatelessSessionForKey(string factoryKey, IStatelessSession session)
        {
            var storage = GetSimpleSessionStorage();
            storage.SetStatelessSessionForKey(factoryKey, session);
        }

        private static SimpleSessionStorage GetSimpleSessionStorage()
        {
            return SessionStorage;
        }
    }
}