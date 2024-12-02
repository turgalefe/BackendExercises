using FluentValidation;
using Project01.Model;
namespace Project01.Validators
{
    public class UserValidator :AbstractValidator <User>
    {
        public UserValidator()
        {

            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");

            RuleFor(c => c.Surname).NotEmpty().WithMessage("Surname is required.");

            RuleFor(c => c.Age)
               .NotEmpty().WithMessage("Age is required.")
               .InclusiveBetween(7, 70).WithMessage("Age must be between 7 and 70.");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(2).WithMessage("Password length must be greater than 2 characters.")
                .MaximumLength(8).WithMessage("Password length must be 6 characters or less.")
                .Matches(@"[A-Za-z]").WithMessage("Password must contain at least one letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.");

        }
    }
}
