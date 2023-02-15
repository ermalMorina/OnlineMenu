using FluentValidation;
using OnlineMenu.Models;
//namespace OnlineMenu.Validations
//{
//    public class UserValidator : AbstractValidator<User>
//    {
//        public UserValidator()
//        {
//            RuleFor(u => u.Username).NotEmpty().WithMessage("Username nuk duhet te jete i zbrazet");
//            RuleFor(u => u.Password).NotEmpty().MinimumLength(8).WithMessage("Password nuk duhet te jete i zbrazet apo me pak se 9 karaktere");
//            RuleFor(u => u.Phone).NotEmpty().WithMessage("Phone nuk duhet te jete i zbrazet");
//            RuleFor(u => u.Email).NotEmpty().WithMessage("Email nuk duhet te jete i zbrazet ");
//            RuleFor(u => u.Email).EmailAddress().WithMessage("Email duhet te jete valid");
//            RuleFor(u => u.Birthdate).Empty().WithMessage("Birthdate nuk duhet te jete i zbrazet");
//            RuleFor(u => u.FirstName).Empty().WithMessage("FirstName nuk duhet te jete i zbrazet");
//            RuleFor(u => u.LastName).Empty().WithMessage("LastName nuk duhet te jete i zbrazet");
//            RuleFor(u => u.ConfirmPassword).Empty().WithMessage("Confirm Password nuk duhet te jete i zbrazet");
//        }
//    }
//}
