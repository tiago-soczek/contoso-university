using System.Threading;
using System.Threading.Tasks;
using Contoso.University.Model.Shared.Services;
using MediatR;

namespace Contoso.University.Model.Shared.Behaviors
{
    public class MediatorDomainEvents : IDomainEvents
    {
        private readonly IMediator mediator;

        public MediatorDomainEvents(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Raise(INotification notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            return mediator.Publish(notification, cancellationToken);
        }
    }
}