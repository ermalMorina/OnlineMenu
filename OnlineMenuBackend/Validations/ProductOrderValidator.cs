using FluentValidation;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class ProductOrderValidator : AbstractValidator<ProductOrderViewModel>
    {
        public ProductOrderValidator()
        {
            RuleFor(u => u.ProductId).NotEmpty().WithMessage("Product nuk duhet te jete i zbrazet ");
            RuleFor(u => u.OrderId).Empty().WithMessage("Order nuk duhet te jete i zbrazet");
            RuleFor(u => u.Quantity).Empty().WithMessage("Quantity nuk duhet te jete i zbrazet");
            RuleFor(u => u.TenantId).Empty().WithMessage("Tenant nuk duhet te jete i zbrazet");
        }
    }
}
