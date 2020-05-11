using System.Collections.Generic;
using System.Reflection;
using NHibernate;
using NHibernate.Criterion;
using YouVote.Common.DomainModel;
using YouVote.Common.NHibernate;

namespace YouVote.Common.PersistenceSupport
{
    public class NHibernateRepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : EntityWithTypedId<TId>
    {
        protected virtual ISession Session
        {
            get
            {
                return NHibernateSession.Current;
            }
        }

        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
        }

        public virtual void Evict(T entity)
        {
            Session.Evict(entity);
        }

        public virtual IList<T> FindAll(IDictionary<string, object> propertyValuePairs)
        {
            var criteria = Session.CreateCriteria(typeof(T));

            foreach (var key in propertyValuePairs.Keys)
            {
                criteria.Add(
                    propertyValuePairs[key] != null
                        ? Restrictions.Eq(key, propertyValuePairs[key])
                        : Restrictions.IsNull(key));
            }

            return criteria.List<T>();
        }

        public virtual IList<T> FindAll(T exampleInstance, params string[] propertiesToExclude)
        {
            var criteria = Session.CreateCriteria(typeof(T));
            var example = Example.Create(exampleInstance);

            foreach (var propertyToExclude in propertiesToExclude)
            {
                example.ExcludeProperty(propertyToExclude);
            }

            criteria.Add(example);

            return criteria.List<T>();
        }

        public virtual T FindOne(IDictionary<string, object> propertyValuePairs)
        {
            var foundList = FindAll(propertyValuePairs);

            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }

            if (foundList.Count == 1)
            {
                return foundList[0];
            }

            return default(T);
        }

        public virtual T FindOne(T exampleInstance, params string[] propertiesToExclude)
        {
            var foundList = FindAll(exampleInstance, propertiesToExclude);

            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }

            if (foundList.Count == 1)
            {
                return foundList[0];
            }

            return default(T);
        }

        public virtual T Get(TId id)
        {
            return Session.Get<T>(id);
        }

        public virtual T Get(TId id, Enums.LockMode lockMode)
        {
            return Session.Get<T>(id, ConvertFrom(lockMode));
        }

        public virtual IEnumerable<T> GetAll()
        {
            var criteria = Session.CreateCriteria(typeof(T));
            return criteria.Future<T>();
        }

        public virtual IEnumerable<T> GetAll(int startRowIndex, int maximumRows)
        {
            return Session.QueryOver<T>()
                .Skip(startRowIndex)
                .Take(maximumRows)
                .Future<T>();
        }

        public int GetAllCount()
        {
            return Session.QueryOver<T>()
                .Select(Projections.RowCount())
                .FutureValue<int>()
                .Value;
        }

        public virtual T Load(TId id)
        {
            return Session.Load<T>(id);
        }

        public virtual T Load(TId id, Enums.LockMode lockMode)
        {
            return Session.Load<T>(id, ConvertFrom(lockMode));
        }

        public virtual TLookupType Load<TLookupType>(object id)
        {
            return Session.Load<TLookupType>(id);
        }

        public virtual IList<T> PerformQuery(IQuery<T> query)
        {
            return query.ExecuteQuery();
        }

        public virtual T Save(T entity)
        {
            Session.Save(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            Session.Update(entity);
            return entity;
        }

        protected static LockMode ConvertFrom(Enums.LockMode lockMode)
        {
            var translatedLockMode = typeof(LockMode).GetField(
                lockMode.ToString(), BindingFlags.Public | BindingFlags.Static);
            return (LockMode)translatedLockMode.GetValue(null);
        }
    }
}