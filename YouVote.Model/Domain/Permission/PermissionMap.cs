using FluentNHibernate.Mapping;

namespace YouVote.Model.Domain.Permission {
    public class PermissionMap : ClassMap<Permission> {

        public PermissionMap()
        {
            Table("Permission");
			LazyLoad();
            Id(x => x.Id).GeneratedBy.Assigned();
        	Map(x => x.Name);
        	Map(x => x.Describe).Nullable();
        }
    }
}
