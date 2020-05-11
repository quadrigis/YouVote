using System.Data;
using NHibernate;
using System;
using YouVote.Common.NHibernate;

namespace YouVote.Common.PersistenceSupport
{
    public class Transaction : INewTransaction
	{
		private readonly string _factoryKey;

		private bool _disposed;

		private readonly ITransaction _transaction;

		private readonly bool _ownTransaction;

		public Transaction(string factoryKey, IsolationLevel isolationLevel)
		{
			_factoryKey = factoryKey;
			var session = NHibernateSession.CurrentFor(_factoryKey);

			_ownTransaction = !session.Transaction.IsActive;

			if (_ownTransaction)
			{
				if (isolationLevel == IsolationLevel.Unspecified)
				{
					_transaction = session.BeginTransaction();
				}
				else
				{
					_transaction = session.BeginTransaction(isolationLevel);
				}
			}
		}


		public Transaction(string factoryKey) : this(factoryKey, IsolationLevel.Unspecified)
		{
		}

		~Transaction()
		{
			Dispose(false);
		}

		public Transaction()
			: this(NHibernateSession.DefaultFactoryKey)
		{
		}

		public void Commit()
		{
			EndTransaction(true);
		}

		public void Rollback()
		{
			EndTransaction(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}
			_disposed = true;

			if (disposing)
			{
				// free managed resources
				Rollback();
			}
		}

		private void EndTransaction(bool commit)
		{
			if (_ownTransaction && _transaction.IsActive)
			{
				if (commit)
				{
					_transaction.Commit();
				}
				else
				{
					_transaction.Rollback();
				}
				_transaction.Dispose();
			}
		}
	}
}