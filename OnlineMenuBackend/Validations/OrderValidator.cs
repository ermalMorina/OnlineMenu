using FluentValidation;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class OrderValidator : AbstractValidator<OrderViewModel>
    {
        public OrderValidator()
        {
            RuleFor(u => u.TableId).NotEmpty().WithMessage("Table nuk duhet te jete e zbrazet");
        }
    }
}
