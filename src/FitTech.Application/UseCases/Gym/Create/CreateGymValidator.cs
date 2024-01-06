using FitTech.Comunication.Requests.Gym;
using FitTech.Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FitTech.Application.UseCases.Gym.Create
{
    public class CreateGymValidator : AbstractValidator<RequestCreateGymDTO>
    {
        public CreateGymValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_NAME);
            
            RuleFor(r => r.EmailAddress).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_EMAIL);
            
            RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PHONE_NUMBER);

            RuleFor(r => r.Address).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_ADDRESS);

            When(r => !string.IsNullOrWhiteSpace(r.EmailAddress), () =>
            {
                RuleFor(r => r.EmailAddress).EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL);
            });

            When(r => !string.IsNullOrWhiteSpace(r.PhoneNumber), () =>
            {
                RuleFor(r => r.PhoneNumber).Custom((phone, context) =>
                {
                    var phonePattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                    var isMatch = Regex.IsMatch(phone, phonePattern);
                    if(!isMatch)
                    {
                        context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(phone), ResourceErrorMessages.INVALID_PHONE_NUMBER));
                    }
                });
            });

            RuleFor(r => r.Address).ChildRules( a=>
            {
                a.RuleFor(r=> r.Country).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_COUNTRY);
                a.RuleFor(r => r.PostalCode).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_POSTAL_CODE);
                a.RuleFor(r => r.State).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_STATE);
                a.RuleFor(r => r.City).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_CITY);
                a.RuleFor(r => r.Street).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_STREET);
                a.RuleFor(r => r.Number).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_NUMBER);

                When(r => !string.IsNullOrWhiteSpace(r.Address.PostalCode), () =>
                {
                    a.RuleFor(r => r.PostalCode).Custom((postalCode, context) =>
                    {
                        var postalCodePattern = "[0-9]{5}-[0-9]{3}";
                        var isMatch = Regex.IsMatch(postalCode, postalCodePattern);
                        if (!isMatch)
                        {
                            context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(postalCode),ResourceErrorMessages.INVALID_POSTAL_CODE));
                        }
                    });

                });

                When(r => !string.IsNullOrEmpty(r.Address.State), () =>
                {
                    a.RuleFor(r => r.State).Length(2).WithMessage(ResourceErrorMessages.INVALID_STATE);
                });
                
            });
        }
    }
}
