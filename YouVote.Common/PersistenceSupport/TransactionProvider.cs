using System.Data;

namespace YouVote.Common.PersistenceSupport
{
    public class TransactionProvider : ITransactionProvider
    {
        public INewTransaction CreateTransaction()
        {
            return new Transaction();
        }

        public INewTransaction CreateTransaction(string factoryKey)
        {
            return new Transaction(factoryKey, IsolationLevel.Unspecified);
        }

        public INewTransaction CreateTransaction(string factoryKey, IsolationLevel isolationLevel)
		{
			return new Transaction(factoryKey, isolationLevel);
		}
    }
}