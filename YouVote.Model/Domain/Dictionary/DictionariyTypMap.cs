using FluentNHibernate.Mapping;

namespace YouVote.Model.Domain.Dictionary
{
    public class DictionaryTypeMap : ClassMap<DictionaryType>
	{
        public DictionaryTypeMap()
		{
            Table("DictionaryType");
			LazyLoad();

            Id(x => x.Id);
            Map(x => x.Name);
		}
	}
}
