using FluentValidation;
using OnlineMenu.Persistence;

namespace OnlineMenu.Validations
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        public ApplicationUserValidator()
        {
            RuleFor(x=>x.FullName).NotNull();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.PasswordHash).NotEmpty();
        }
        public ApplicationUserValidator(string s1)
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}
