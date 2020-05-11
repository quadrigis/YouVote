using System.Collections.Generic;
using NHibernate;

namespace YouVote.Common.NHibernate
{
    public interface ISessionStorage
    {
        IEnumerable<ISession> GetAllSessions();

        ISession GetSessionForKey(string factoryKey);

        void SetSessionForKey(string factoryKey, ISession session);

        IEnumerable<IStatelessSession> GetAllStatelessSessions();

        IStatelessSession GetStatelessSessionForKey(string factoryKey);

        void SetStatelessSessionForKey(string factoryKey, IStatelessSession session);
    }
}
