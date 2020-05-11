using FluentNHibernate.Mapping;

namespace YouVote.Model.Domain.Dictionary
{
    public class PersonalityNumberTypeMap : SubclassMap<PersonalityNumberType>
	{
        public PersonalityNumberTypeMap()
		{
			DiscriminatorValue(1);
		}
	}
}
