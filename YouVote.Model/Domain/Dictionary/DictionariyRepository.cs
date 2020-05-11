using System.Collections.Generic;
using NHibernate;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.Dictionary
{
    public class DictionaryRepository : NHibernateRepository<Dictionary>, IDictionaryRepository
	{
        public IEnumerable<T> GetActiveAll<T>() where T : Dictionary
		{
			return Session.QueryOver<T>()
                .Where(x=>x.IsActive)
				.OrderBy(x => x.Order).Asc
				.Future<T>();
		}

        public T Get<T>(int id) where T : Dictionary
        {
            return Session.QueryOver<T>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public IEnumerable<T> GetAll<T>() where T : Dictionary
		{
			return Session.QueryOver<T>()
				.OrderBy(x => x.Order).Asc
				.Cacheable()
				.CacheMode(CacheMode.Normal)
				.Future<T>();
		}
	}
}
