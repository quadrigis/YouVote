using System.Collections.ObjectModel;
using System.Linq;
using Iesi.Collections.Generic;
using YouVote.Common.DomainModel;
using YouVote.Model.Domain.Dictionary;

namespace YouVote.Model.Domain.User
{
    public class User : Entity
    {
		public virtual string Email { get; set; }
		public virtual string Password { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Address { get; set; }
        public virtual int? Age { get; set; }
        public virtual string PersonalityNumber { get; set; }
        public virtual PersonalityNumberType PersonalityNumberType { get; set; }

        public virtual int RecordVersion { get; set; }
        public virtual bool IsActive { get; set; }

        private readonly ISet<Role.Role> _roles = new HashedSet<Role.Role>();

        public virtual ReadOnlyCollection<Role.Role> Roles
        {
            get
            {
                return new ReadOnlyCollection<Role.Role>(_roles.ToList());
            }
        }

        public virtual void AddRole(Role.Role p)
        {
            _roles.Add(p);
        }

        public virtual void RemoveRole(Role.Role p)
        {
            _roles.Remove(p);
        }
    }
}
