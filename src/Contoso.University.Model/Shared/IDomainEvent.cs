using MediatR;
using Zek.Model;

namespace Contoso.University.Model.Shared
{
    public interface IDomainEvent : INotification
    {
        BaseEntity Entity { get; }
    }
}
