using FluentNHibernate.Mapping;

namespace YouVote.Model.Domain.Role {
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("SysRole");
			LazyLoad();
            Id(x => x.Id).GeneratedBy.HiLo("HiLo", "NextValue", "10", "TableName = 'SysRole'").Column("id");
        	Map(x => x.Name);
        	Map(x => x.Describe);
        	Version(x => x.RecordVersion);

            HasManyToMany(x => x.Permissions)
                .Table("SysRolePermission")
                .ParentKeyColumn("SysRoleId")
                .ChildKeyColumn("PermissionId")
                .AsSet()
                .Inverse()
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.None();
        }
    }
}
