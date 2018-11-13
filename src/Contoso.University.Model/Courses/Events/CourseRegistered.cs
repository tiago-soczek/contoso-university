using MediatR;

namespace Contoso.University.Model.Courses.Events
{
    public class CourseRegistered : INotification
    {
        public CourseRegistered(Course course)
        {
            Course = course;
        }

        public Course Course { get; }        
    }
}
