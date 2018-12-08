using System.Threading;
using System.Threading.Tasks;
using Contoso.University.Model.Students.Commands;
using MediatR;
using Zek.Model;

namespace Contoso.University.Model.Students.Handlers
{
    public class StudentsHandler : IRequestHandler<RegisterStudentCommand, Result>
    {
        public Task<Result> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success);
        }
    }
}
