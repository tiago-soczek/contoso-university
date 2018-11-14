using System.Threading;
using System.Threading.Tasks;
using Contoso.University.Model.AccessControl.Services;
using Contoso.University.Model.Shared;
using Contoso.University.Model.Shared.Repositories;
using Contoso.University.Model.Shared.Services;
using MediatR;
using Newtonsoft.Json;
using Zek.Model;

namespace Contoso.University.Model.AccessControl.Behaviors
{
    public class MediatorDomainEvents : IDomainEvents
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserService currentUserService;
        private readonly IRepository<UserEvent> userEventRepository;

        public MediatorDomainEvents(IMediator mediator, ICurrentUserService currentUserService, IRepository<UserEvent> userEventRepository)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
            this.userEventRepository = userEventRepository;
        }

        public async Task Raise(IDomainEvent domainEvent, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Save(domainEvent);

            await mediator.Publish(domainEvent, cancellationToken);
        }

        private async Task Save(IDomainEvent domainEvent)
        {
            var userEvent = new UserEvent
            {
                User = await currentUserService.GetCurrentUser(),
                Entity = domainEvent.Entity.GetType().Name,
                EntityId = domainEvent.Entity.Id,
                Details = JsonConvert.SerializeObject(domainEvent.Entity),
                EventName = domainEvent.GetType().Name,
                Timestamp = Current.Now
            };

            await userEventRepository.Insert(userEvent);
        }
    }
}