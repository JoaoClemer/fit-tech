using FitTech.Application.UseCases.Address;
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

            RuleFor(r => r.Address).SetValidator(new RegisterAddressValidator());

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

        }
    }
}
