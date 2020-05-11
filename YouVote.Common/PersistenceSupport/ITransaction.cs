using System;

namespace YouVote.Common.PersistenceSupport
{
    public interface INewTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}