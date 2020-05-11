using System.Collections.ObjectModel;
using System.Linq;
using Iesi.Collections.Generic;
using YouVote.Common.DomainModel;

namespace YouVote.Model.Domain.Role
{
    public class Role : Entity
    {
		public virtual string Name { get; set; }
		public virtual string Describe { get; set; }
		public virtual int RecordVersion { get; set; }

        private readonly ISet<Permission.Permission> _permissions = new HashedSet<Permission.Permission>();

        public virtual ReadOnlyCollection<Permission.Permission> Permissions
        {
            get
            {
                return new ReadOnlyCollection<Permission.Permission>(_permissions.ToList());
            }
        }

        public virtual void AddPermission(Permission.Permission p)
        {
            _permissions.Add(p);
        }

        public virtual void RemovePermission(Permission.Permission p)
        {
            _permissions.Remove(p);
        }
    }
}
