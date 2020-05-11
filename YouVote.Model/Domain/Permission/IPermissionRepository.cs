using System.Collections.Generic;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.Permission
{
    public interface IPermissionRepository : IRepository<Permission>
	{
        IEnumerable<Permission> GetByName(string name);
	}
}
