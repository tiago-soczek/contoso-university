using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Contoso.University.Model.Shared.Behaviors
{
    public class AuditRequestsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            // Before

            return next();

            // After
        }
    }
}