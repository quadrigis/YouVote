using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.Dictionary
{
	public class DictionaryDataObject : IDictionaryDataObject
	{
		private readonly ITransactionProvider _transactionProvider;

        public DictionaryDataObject(ITransactionProvider transactionProvider)
		{
			_transactionProvider = transactionProvider;
		}
	}
}
