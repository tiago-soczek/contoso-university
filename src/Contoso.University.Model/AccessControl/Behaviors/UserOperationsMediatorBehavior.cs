using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Contoso.University.Model.AccessControl.Behaviors
{
    public class UserOperationsMediatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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