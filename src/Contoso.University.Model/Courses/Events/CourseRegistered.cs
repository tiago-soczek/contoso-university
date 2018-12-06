using Contoso.University.Model.Shared;
using Zek.Model;

namespace Contoso.University.Model.Courses.Events
{
    public class CourseRegistered : IDomainEvent
    {
        public CourseRegistered(Course course)
        {
            Entity = course;
        }

        public BaseEntity Entity { get; }
    }
}
