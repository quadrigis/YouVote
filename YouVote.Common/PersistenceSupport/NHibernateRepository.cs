using YouVote.Common.DomainModel;

namespace YouVote.Common.PersistenceSupport
{
    public class NHibernateRepository<T> : NHibernateRepositoryWithTypedId<T, int>, IRepository<T> where T : Entity
    {
    }
}