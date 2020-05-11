using System.Collections.Generic;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.User
{
    public interface IUserRepository : IRepository<User>
	{
        User GetByEmail(string email);

        bool IsUnique(string email);
	}
}
