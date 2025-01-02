using FluentValidation;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories;

namespace PlayFieldBuddy.Api.Validation
{
    public class UserCreateRequestValidator :AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator(PlayFieldBuddyDbContext dbContext)
        {
            RuleFor(x => x.Mail)
                   .Custom((value, context) =>
                   {
                       var emailInUse = dbContext.Users.Any(u => u.Mail == value);
                       if (emailInUse)
                       {
                           context.AddFailure("Mail", "that email is taken");
                       }
                   });

        }

    }
}
