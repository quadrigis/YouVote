using FluentNHibernate.Mapping;

namespace YouVote.Model.Domain.User {
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("SysUser");
			LazyLoad();
            Id(x => x.Id).GeneratedBy.HiLo("HiLo", "NextValue", "10", "TableName = 'SysUser'").Column("id");
        	Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.PersonalityNumber).Nullable();
            References(x => x.PersonalityNumberType).Column("PersonalityNumberTypeId").Nullable();
            Map(x => x.LastName).Nullable();
            Map(x => x.FirstName).Nullable();
            Map(x => x.Age).Nullable();
            Map(x => x.Address).Nullable();
            Map(x => x.IsActive);

        	Version(x => x.RecordVersion);

            HasManyToMany(x => x.Roles)
                .Table("SysUserSysRole")
                .ParentKeyColumn("SysUserId")
                .ChildKeyColumn("SysRoleId")
                .AsSet()
                .Inverse()
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.None();
        }
    }
}
