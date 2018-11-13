using System.Threading.Tasks;
using AutoMapper;
using Contoso.University.Api.Courses.Dtos;
using Contoso.University.Model.Courses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zek.Api;

namespace Contoso.University.Api.Courses
{
    [Route(RouteConstants.Controller)]
    public class CoursesController : BaseController
    {
        public CoursesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> Register(RegisterCourseCommand cmd)
        {
            var result = await Mediator.Send(cmd);

            return As<CourseDto>(result);
        }
    }
}
