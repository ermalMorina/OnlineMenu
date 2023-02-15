using FluentValidation;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class ResetPasswordViewModelValidator : AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordViewModelValidator()
        {
            RuleFor(x=> x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.token).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).NotEmpty().MinimumLength(8);
        }
    }
}
