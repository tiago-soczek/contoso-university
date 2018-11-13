using System;
using Contoso.University.Model.Courses.Commands;
using Zek.Model;

namespace Contoso.University.Model.Courses
{
    public class Course
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset RegisteredAt { get; private set; }

        public static Course From(RegisterCourseCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new Course
            {
                Title = request.Title,
                Description = request.Title,
                RegisteredAt = Current.Now
            };
        }
    }
}
