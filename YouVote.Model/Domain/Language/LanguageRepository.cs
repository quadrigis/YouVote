using System.Collections.Generic;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Domain.Language
{
    public class LanguageRepository : NHibernateRepository<Language>, ILanguageRepository
    {
        
    }
}
