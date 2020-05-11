using System.Collections.Generic;
using System.Reflection;

namespace YouVote.Common.DomainModel
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
        IEnumerable<PropertyInfo> GetSignatureProperties();
        bool IsTransient();
    }
}
