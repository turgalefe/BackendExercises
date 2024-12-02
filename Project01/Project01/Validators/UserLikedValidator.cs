using FluentValidation;
using Project01.Model;

namespace Project01.Validators
{
    public class UserLikedValidator : AbstractValidator <UserLiked>
    {
        public UserLikedValidator() 
        {
            RuleFor(x => x.IsLiked)
                .Must(x => x == true || x == false)
                .WithMessage("IsLiked must be either true or false.");

            RuleFor(x => x.Ratings)
                .InclusiveBetween(1, 10).WithMessage("Ratings must be between 1 and 10.");

            RuleFor(x => x.Comments)
               .MaximumLength(100).WithMessage("Comments must not exceed 100 characters.");

            RuleFor(x => x.PostComments)
               .MaximumLength(100).WithMessage("PostComments must not exceed 100 characters.");
        }
    }
}
