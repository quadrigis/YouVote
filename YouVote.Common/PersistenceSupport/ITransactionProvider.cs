using System.Data;

namespace YouVote.Common.PersistenceSupport
{
    public interface ITransactionProvider
    {
        INewTransaction CreateTransaction();

        INewTransaction CreateTransaction(string factoryKey);

		INewTransaction CreateTransaction(string factoryKey, IsolationLevel isolationLevel);
    }
}