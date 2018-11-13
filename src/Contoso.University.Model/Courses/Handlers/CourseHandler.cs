using System.Threading;
using System.Threading.Tasks;
using Contoso.University.Model.Courses.Commands;
using Contoso.University.Model.Courses.Events;
using Contoso.University.Model.Courses.Repositories;
using MediatR;
using Zek.Model;

namespace Contoso.University.Model.Courses.Handlers
{
    public class CoursesAppService : IRequestHandler<RegisterCourseCommand, Result<Course>>
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMediator mediator;

        public CoursesAppService(ICourseRepository courseRepository, IMediator mediator)
        {
            this.courseRepository = courseRepository;
            this.mediator = mediator;
        }

        public async Task<Result<Course>> Handle(RegisterCourseCommand request, CancellationToken cancellationToken)
        {
            var course = Course.From(request);

            await courseRepository.Insert(course);

            await mediator.Publish(new CourseRegistered(course));

            return Result.Ok(course);
        }
    }
}
