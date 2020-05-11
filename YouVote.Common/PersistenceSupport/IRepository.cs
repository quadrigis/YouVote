namespace YouVote.Common.PersistenceSupport
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, int>
    {
    }
}