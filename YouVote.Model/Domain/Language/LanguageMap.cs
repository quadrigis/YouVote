using FluentNHibernate.Mapping;

namespace YouVote.Model.Domain.Language
{
    public class LanguageMap : ClassMap<Language>
    {
        public LanguageMap()
        {
            Table("Language");
			LazyLoad();
            Id(x => x.Id).GeneratedBy.Assigned();
        	Map(x => x.Name);
        }
    }
}
