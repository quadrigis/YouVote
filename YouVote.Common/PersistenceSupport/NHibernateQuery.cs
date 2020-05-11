using System.Collections.Generic;
using NHibernate;
using YouVote.Common.NHibernate;

namespace YouVote.Common.PersistenceSupport
{
    public abstract class NHibernateQuery<T> : IQuery<T>
    {
        protected virtual ISession Session
        {
            get
            {
                return NHibernateSession.Current;
            }
        }

        public abstract IList<T> ExecuteQuery();
    }
}