using System.Threading.Tasks;
using Contoso.University.Model.Courses;
using Contoso.University.Model.Courses.Repositories;

namespace Contoso.University.Infra.Courses.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public Task Insert(Course course)
        {
            return Task.CompletedTask;
        }
    }
}
