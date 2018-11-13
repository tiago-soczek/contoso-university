using System.Threading.Tasks;

namespace Contoso.University.Model.Courses.Repositories
{
    public interface ICourseRepository
    {
        Task Insert(Course course);
    }
}
