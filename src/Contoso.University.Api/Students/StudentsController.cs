using System.Threading.Tasks;
using AutoMapper;
using Contoso.University.Model.Students.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zek.Api;

namespace Contoso.University.Api.Students
{
    [Route(RouteConstants.Controller)]
    public class StudentsController : BaseController
    {
        public StudentsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterStudentCommand cmd)
        {
            var result = await Mediator.Send(cmd);

            return AsResult(result);
        }
    }
}
