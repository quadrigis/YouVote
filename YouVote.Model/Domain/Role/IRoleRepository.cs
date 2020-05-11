using System.Collections.Generic;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.Role
{
    public interface IRoleRepository : IRepository<Role>
	{
        IEnumerable<Role> GetByName(string name);
	}
}
