using System.Collections.Generic;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.Dictionary
{
    public interface IDictionaryRepository : IRepository<Dictionary>
	{
        IEnumerable<T> GetAll<T>() where T : Dictionary;

        IEnumerable<T> GetActiveAll<T>() where T : Dictionary;

        T Get<T>(int id) where T : Dictionary;
	}
}


