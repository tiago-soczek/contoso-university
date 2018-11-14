using System.Threading;
using System.Threading.Tasks;
using Contoso.University.Model.Courses.Commands;
using Contoso.University.Model.Courses.Events;
using Contoso.University.Model.Courses.Repositories;
using Contoso.University.Model.Shared.Services;
using MediatR;
using Zek.Model;

namespace Contoso.University.Model.Courses.Handlers
{
    public class CoursesAppService : IRequestHandler<RegisterCourseCommand, Result<Course>>
    {
        private readonly ICourseRepository courseRepository;
        private readonly IDomainEvents domainEvents;

        public CoursesAppService(ICourseRepository courseRepository, IDomainEvents domainEvents)
        {
            this.courseRepository = courseRepository;
            this.domainEvents = domainEvents;
        }

        public async Task<Result<Course>> Handle(RegisterCourseCommand request, CancellationToken cancellationToken)
        {
            var course = Course.From(request);

            await courseRepository.Insert(course);

            await domainEvents.Raise(new CourseRegistered(course), cancellationToken);

            return Result.Ok(course);
        }
    }
}
