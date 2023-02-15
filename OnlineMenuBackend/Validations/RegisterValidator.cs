using FluentValidation;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Emri i plotë nuk duhet te jetë i zbrazet");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email nuk duhet te jetë i zbrazet");
            RuleFor(x => x.Username).NotEmpty().MinimumLength(5).WithMessage("Username nuk duhet te jetë i zbrazet apo me pak se 5 karaktere");
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).WithMessage("Numri i telefonit nuk duhet te jetë i zbrazet apo me pak se 8 karaktere");
            RuleFor(x => x.TenantId).NotEmpty().WithMessage("TenantId nuk duhet te jete i zbrazet");
            RuleFor(x => x.Password).MinimumLength(6).NotEmpty().WithMessage("Password nuk duhet te jetë i zbrazet ose me pak se 6 karaktere");
            //RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Ditëlindja nuk duhet te jetë e zbrazet");
            RuleFor(x => x.Roles).NotEmpty().WithMessage("Rolet nuk duhet te jetë e zbrazet");
        }
    }
}
