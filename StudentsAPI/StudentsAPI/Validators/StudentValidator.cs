using StudentsAPI.Model;
using FluentValidation;
namespace StudentsAPI.Validators
{
    public class StudentValidator : AbstractValidator<Students>
    {
        public StudentValidator() 
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => x.Password).Must(x => x == null || x.Length >= 2).WithMessage("Password length must be greater than 2");
        }
    }
}
