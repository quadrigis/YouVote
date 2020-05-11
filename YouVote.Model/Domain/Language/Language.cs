using System.Collections.ObjectModel;
using System.Linq;
using Iesi.Collections.Generic;
using YouVote.Common.DomainModel;

namespace YouVote.Model.Domain.Language
{
    public class Language : Entity
    {
		public virtual string Name { get; set; }
    }
}
