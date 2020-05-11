using System;
using YouVote.Common.DomainModel;

namespace YouVote.Model.Domain.Audyt
{
	public class AudytLog : Entity
	{
		public virtual DateTime LogDateTime { get; set; }

		public virtual string UserLogin { get; set; }

		public virtual int AudytLogType { get; set; }

		public virtual string ObjectType { get; set; }

		public virtual string ObjectId { get; set; }
		
		public virtual string ObjectIdType { get; set; }

		public virtual string ObjectValue { get; set; }

		public virtual AudytLogType AudytLogTypeEnum
		{
			get { return (AudytLogType)AudytLogType; }
			set { AudytLogType = (int)value; }
		}

	}
}