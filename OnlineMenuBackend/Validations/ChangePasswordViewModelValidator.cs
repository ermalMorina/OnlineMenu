using FluentValidation;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(x=>x.Username).NotEmpty();
            RuleFor(x => x.CurrentPassword).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).NotEmpty().MinimumLength(8);
        }
    }
}
