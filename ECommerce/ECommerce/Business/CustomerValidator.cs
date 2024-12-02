using ECommerce.DataAccess;
using FluentValidation;

namespace ECommerce.Business
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name cannot be longer than 100 characters.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Matches(@"^[a-zA-Z0-9]*$").WithMessage("Password must contain only letters and numbers.")
                .MaximumLength(15).WithMessage("Password cannot be longer than 15 characters.");
        }
    }

}
