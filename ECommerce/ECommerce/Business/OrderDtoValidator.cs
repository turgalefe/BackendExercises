using ECommerce.DataAccess;
using FluentValidation;
using System;
using System.Globalization;

namespace ECommerce.Business
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("Order date is required.") // Ensures the date is not empty
                .Must(BeAValidDate).WithMessage("Order date must be a valid date in the format yyyy-MM-dd.") // Ensures the date is in a valid format
                .Must(BeAValidFutureDate).WithMessage("Order date cannot be in the past."); // Ensures the order date is not in the past

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than zero."); // Ensures the total amount is positive

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Customer ID must be greater than zero."); // Ensures the customer ID is a positive number
        }

        private bool BeAValidDate(string orderDate)
        {
            return DateTime.TryParseExact(orderDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private bool BeAValidFutureDate(string orderDate)
        {
            if (DateTime.TryParseExact(orderDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate >= DateTime.Today; // Gelecek tarih kontrolü
            }
            return true; // Eğer tarih geçersizse, diğer kurallar geçersiz sayılır
        }
    }
}
