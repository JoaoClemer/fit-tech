using FitTech.Comunication.Requests.Login;
using FitTech.Exceptions;
using FluentValidation;

namespace FitTech.Application.UseCases.Login.DoLogin
{
    public class DoLoginValidator : AbstractValidator<RequestDoLoginDTO>
    {
        public DoLoginValidator()
        {
            RuleFor(r => r.EmailAddress).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_EMAIL);

            RuleFor(r => r.Password).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PASSWORD);

            RuleFor(r => r.UserType).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_USER_TYPE);

            When(r => !string.IsNullOrWhiteSpace(r.EmailAddress), () =>
            {
                RuleFor(r => r.EmailAddress).EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL);
            });

            When(r => !string.IsNullOrWhiteSpace(r.Password), () =>
            {
                RuleFor(r => r.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErrorMessages.INVALID_PASSWORD);
            });
        }
    }
}
