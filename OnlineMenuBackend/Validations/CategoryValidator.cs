using FluentValidation;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name nuk duhet te jete i zbrazet");
        }
    }
}
