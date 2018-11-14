using System.Threading.Tasks;

namespace Contoso.University.Model.AccessControl.Services
{
    public interface ICurrentUserService
    {
        Task<User> GetCurrentUser();
    }
}
