using ECommerce.DataAccess;
using FluentValidation;

namespace ECommerce.Business
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            //RuleFor(o => o.OrderDate)
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future.");

            RuleFor(o => o.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than zero.");

            RuleFor(o => o.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");
        }
    }
}