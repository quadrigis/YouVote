using FluentNHibernate.Mapping;

namespace YouVote.Model.Domain.Dictionary
{
    public class DictionaryMap : ClassMap<Dictionary>
	{
        public DictionaryMap()
		{
			Cache.NonStrictReadWrite();
            Table("Dictionary");
			LazyLoad();

		    Id(x => x.Id).GeneratedBy.Assigned();

            Map(x => x.IsActive);
            Map(x => x.Order);
            Map(x => x.ShortValue);
            Map(x => x.Value);

            References(x => x.DictionaryType).Column("DictionaryTypeId");
            References(x => x.Language).Column("LanguageId");

            DiscriminateSubClassesOnColumn("DictionaryTypeId").ReadOnly();
		}
	}
}
