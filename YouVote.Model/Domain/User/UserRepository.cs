using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.User
{
    public class UserRepository : NHibernateRepository<User>, IUserRepository
    {
        public User GetByEmail(string email)
		{
            var u = Session.QueryOver<User>()
                .Where(y => y.Email == email)
                .SingleOrDefault();
            return u;
		}

        public bool IsUnique(string email)
        {
            var u = Session.QueryOver<User>()
                .Where(y => y.Email == email)
                .RowCount();
            return u == 0;
        }
    }
}
