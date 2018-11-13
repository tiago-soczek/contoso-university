using System;

namespace Contoso.University.Api.Courses.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset RegisteredAt { get; set; }
    }
}
