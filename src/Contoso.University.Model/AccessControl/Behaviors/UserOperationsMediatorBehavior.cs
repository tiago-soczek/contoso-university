using System.Threading;
using System.Threading.Tasks;
using Contoso.University.Model.AccessControl.Services;
using Contoso.University.Model.Shared.Repositories;
using Contoso.University.Model.Shared.Services;
using MediatR;
using Newtonsoft.Json;

namespace Contoso.University.Model.AccessControl.Behaviors
{
    public class UserOperationsMediatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IRepository<UserOperation> userOperationsRepository;
        private readonly IDiagnosticsService diagnosticsService;
        private readonly ICurrentUserService currentUserService;
        private readonly string operationName;

        public UserOperationsMediatorBehavior(IRepository<UserOperation> userOperationsRepository, ICurrentUserService currentUserService, IDiagnosticsService diagnosticsService)
        {
            this.userOperationsRepository = userOperationsRepository;
            this.currentUserService = currentUserService;
            this.diagnosticsService = diagnosticsService;
            operationName = typeof(TRequest).Name;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var operation = await StartNew(request);

            try
            {
                var response = await next();

                operation.FinishAsSuccess();

                return response;
            }
            catch
            {
                operation.FinishAsError();

                throw;
            }
            finally
            {
                await userOperationsRepository.Insert(operation);
            }
        }

        private async Task<UserOperation> StartNew(TRequest request)
        {
            var operation = UserOperation.StartNew();

            operation.OperationName = operationName;
            operation.Request = JsonConvert.SerializeObject(request);
            operation.CorrelationId = diagnosticsService.GetCorrelationId();
            operation.User = await currentUserService.GetCurrentUser();

            return operation;
        }
    }
}