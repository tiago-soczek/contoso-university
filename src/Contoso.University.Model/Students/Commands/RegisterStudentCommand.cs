using System;
using FluentValidation;
using MediatR;
using Zek.Model;

namespace Contoso.University.Model.Students.Commands
{
    public class RegisterStudentCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public StudentAddress Address { get; set; }

        public class StudentAddress
        {
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string ZipCode { get; set; }

            public class Validator : AbstractValidator<StudentAddress>
            {
                public Validator()
                {
                    RuleFor(x => x.Line1).NotEmpty();
                    RuleFor(x => x.Line2).NotEmpty();
                    RuleFor(x => x.City).NotEmpty();
                    RuleFor(x => x.State).NotEmpty();
                    RuleFor(x => x.Country).NotEmpty();
                    RuleFor(x => x.ZipCode).NotEmpty();
                }
            }
        }

        public class Validator : AbstractValidator<RegisterStudentCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Birthday).NotEmpty();
                RuleFor(x => x.Address).NotNull();
            }
        }
    }
}