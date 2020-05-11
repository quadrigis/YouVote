using YouVote.Common.DomainModel;

namespace YouVote.Model.Domain.Audyt
{
	public abstract class AuditableWithTypedId<T> : EntityWithTypedId<T>, IAuditable
	{
		public virtual string IdString { get { return Id.ToString(); } }

		public virtual string IdType
		{
			get { return Id.GetType().Name; }
		}

		public virtual string ObjectType
		{
			get { return GetTypeUnproxied().Name; }
		}
	}

	public abstract class Auditable : Entity, IAuditable
	{
		public virtual string IdString { get { return Id.ToString("G"); } }

		public virtual string IdType
		{
			get { return Id.GetType().Name; }
		}

		public virtual string ObjectType
		{
			get { return GetTypeUnproxied().Name; }
		}
	}
}