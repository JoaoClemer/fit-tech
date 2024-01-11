using FitTech.Comunication.Requests.Address;
using FitTech.Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FitTech.Application.UseCases.Address
{
    public class RegisterAddressValidator : AbstractValidator<RequestRegisterAddressDTO>
    {
        public RegisterAddressValidator()
        {
            RuleFor(r => r.Country).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_COUNTRY);
            RuleFor(r => r.Street).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_STREET);
            RuleFor(r => r.PostalCode).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_POSTAL_CODE);
            RuleFor(r => r.State).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_STATE);
            RuleFor(r => r.City).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_CITY);
            RuleFor(r => r.Number).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_NUMBER);

            When(r => !string.IsNullOrWhiteSpace(r.PostalCode), () =>
            {
                RuleFor(r => r.PostalCode).Custom((postalCode, context) =>
                {
                    var postalCodePattern = "[0-9]{5}-[0-9]{3}";
                    var isMatch = Regex.IsMatch(postalCode, postalCodePattern);
                    if (!isMatch)
                    {
                        context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(postalCode), ResourceErrorMessages.INVALID_POSTAL_CODE));
                    }
                });

            });

            When(r => !string.IsNullOrEmpty(r.State), () =>
            {
                RuleFor(r => r.State).Length(2).WithMessage(ResourceErrorMessages.INVALID_STATE);
            });
        }
    }
}
