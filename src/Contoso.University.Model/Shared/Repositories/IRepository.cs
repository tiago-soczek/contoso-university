using System.Threading.Tasks;
using Zek.Model;

namespace Contoso.University.Model.Shared.Repositories
{
    public interface IRepository<in T> where T : BaseEntity
    {
        Task Insert(T entity);
    }
}
