using System.Threading;
using System.Threading.Tasks;

namespace Contoso.University.Model.Shared.Services
{
    public interface IDomainEvents
    {
        Task Raise(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
