using System.Collections.Generic;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.Permission
{
    public class PermissionRepository : NHibernateRepository<Permission>, IPermissionRepository
    {
        public IEnumerable<Permission> GetByName(string name)
		{
            var pers = Session.QueryOver<Permission>()
				.Where(y => y.Name == name);
            return pers.Future<Permission>();
		}
    }
}
