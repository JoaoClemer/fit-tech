using FitTech.Comunication.Requests.Login;
using FitTech.Exceptions;
using FluentValidation;

namespace FitTech.Application.UseCases.Login.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(r => r.NewPassword).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PASSWORD);

            When(r => !string.IsNullOrWhiteSpace(r.NewPassword), () =>
            {
                RuleFor(r => r.NewPassword.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErrorMessages.INVALID_PASSWORD);
            });
        }
    }
}
