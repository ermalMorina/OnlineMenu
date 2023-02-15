using FluentValidation;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class TableValidator : AbstractValidator<TableViewModel>
    {
        public TableValidator()
        {
            RuleFor(u => u.Number).NotEmpty().WithMessage("Numri i tavolines nuk duhet te jete i zbrazet");
            RuleFor(u => u.TenantId).NotEmpty().WithMessage("Tenant nuk duhet te jete e zbrazet");
        }
    }
}
