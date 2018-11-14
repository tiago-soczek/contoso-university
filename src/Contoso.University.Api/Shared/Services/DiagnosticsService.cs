using System.Diagnostics;
using Contoso.University.Model.Shared.Services;
using Microsoft.AspNetCore.Http;

namespace Contoso.University.Api.Shared.Services
{
    public class DiagnosticsService : IDiagnosticsService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public DiagnosticsService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetCorrelationId() => Activity.Current?.Id ?? httpContextAccessor.HttpContext.TraceIdentifier;
    }
}
