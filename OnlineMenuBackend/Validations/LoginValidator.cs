using FluentValidation;
using OnlineMenu.Persistence;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Username).NotEmpty().WithMessage("Username nuk duhet te jete i zbrazet");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password nuk duhet te jete i zbrazet");
            RuleFor(u => u.TenantId).NotEmpty().WithMessage("TenantId nuk duhet te jete i zbrazet");
        }
    }
}
