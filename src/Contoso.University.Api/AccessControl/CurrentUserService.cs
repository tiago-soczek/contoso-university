using System.Threading.Tasks;
using Contoso.University.Model.AccessControl;
using Contoso.University.Model.AccessControl.Services;
using Zek.Model;

namespace Contoso.University.Api.AccessControl
{
    public class CurrentUserService : ICurrentUserService
    {
        public Task<User> GetCurrentUser() => Task.FromResult(User.System);
    }
}
