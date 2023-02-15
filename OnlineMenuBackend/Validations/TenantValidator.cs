using FluentValidation;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class TenantValidator : AbstractValidator<TenantViewModel>
    {
        public TenantValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name nuk duhet te jete i zbrazet");
            RuleFor(u => u.Identifier).NotEmpty().WithMessage("Subdomain nuk duhet te jete i zbrazet");
        }
    }
}
