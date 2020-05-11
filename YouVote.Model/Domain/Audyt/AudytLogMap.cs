using FluentNHibernate.Mapping;

namespace YouVote.Model.Domain.Audyt
{
	public class LogMap : ClassMap<AudytLog>
	{
		public LogMap()
		{
			Table("Audyt");
			LazyLoad();
			Id(x => x.Id).GeneratedBy.HiLo("HiLo", "NextValue", "10", "TableName = 'Audyt'").Column("id");
		    Map(x => x.LogDateTime);
            Map(x => x.UserLogin).Nullable(); ;
		    Map(x => x.AudytLogType);
            Map(x => x.ObjectType).Nullable(); ;
            Map(x => x.ObjectId).Nullable(); ;
			Map(x => x.ObjectIdType).Nullable();
		    Map(x => x.ObjectValue).Nullable();
		}
	}
}