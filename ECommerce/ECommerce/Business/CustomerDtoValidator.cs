using ECommerce.DataAccess;
using FluentValidation;

public class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");
    }
}
