using FluentValidation;
using MediatR;
using Zek.Model;

namespace Contoso.University.Model.Courses.Commands
{
    public class RegisterCourseCommand : IRequest<Result<Course>>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public class Validator : AbstractValidator<RegisterCourseCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }
    }
}
