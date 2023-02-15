using FluentValidation;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name nuk duhet te jete i zbrazet");
            RuleFor(u => u.Price).NotEmpty().WithMessage("Price nuk duhet te jete i zbrazet");
            RuleFor(u => u.TenantId).NotEmpty().WithMessage("Tenant nuk duhet te jete i zbrazet");
            RuleFor(u => u.Photo).NotEmpty().WithMessage("Photo nuk duhet te jete i zbrazet");
        }   
    }
}
