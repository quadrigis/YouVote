using System.Collections.Generic;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.Role
{
    public class RoleRepository : NHibernateRepository<Role>, IRoleRepository
    {
        public IEnumerable<Role> GetByName(string name)
		{
            var pers = Session.QueryOver<Role>()
				.Where(y => y.Name == name);
            return pers.Future<Role>();
		}
    }
}
