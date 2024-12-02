using ECommerce.DataAccess;
using FluentValidation;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(p => p.Name)
              .NotEmpty().WithMessage("Product name is required.")
              .MaximumLength(200).WithMessage("Product name cannot be longer than 200 characters.")
              .Must(name => char.IsUpper(name[0])).WithMessage("Product name must start with an uppercase letter.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

    }
}
