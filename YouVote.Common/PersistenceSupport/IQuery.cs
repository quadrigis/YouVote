using System.Collections.Generic;

namespace YouVote.Common.PersistenceSupport
{
    public interface IQuery<T>
    {
        IList<T> ExecuteQuery();
    }
}