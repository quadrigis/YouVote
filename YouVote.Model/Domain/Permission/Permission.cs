using YouVote.Common.DomainModel;

namespace YouVote.Model.Domain.Permission {
    public class Permission : Entity
    {
		public virtual string Name { get; set; }
		public virtual string Describe { get; set; }
    }
}
