using FitTech.Application.UseCases.Address;
using FitTech.Comunication.Requests.Employee;
using FitTech.Exceptions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FitTech.Application.UseCases.Employee.Create
{
    public class CreateEmployeeValidator : AbstractValidator<RequestCreateEmployeeDTO>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_NAME);

            RuleFor(r => r.Cpf).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_CPF);

            RuleFor(r => r.EmailAddress).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_EMAIL);

            RuleFor(r => r.Salary).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_SALARY);

            RuleFor(r => r.EmployeeType).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_EMPLOYEE_TYPE);

            RuleFor(r => r.Password).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PASSWORD);

            RuleFor(r => r.Address).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_ADDRESS);

            RuleFor(r => r.GymId).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_GYM_ID);

            RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage(ResourceErrorMessages.EMPTY_PHONE_NUMBER);

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
                    if (!isMatch)
                    {
                        context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(phone), ResourceErrorMessages.INVALID_PHONE_NUMBER));
                    }
                });
            });

            When(r => !string.IsNullOrWhiteSpace(r.Cpf), () =>
            {
                RuleFor(r => r.Cpf).Custom((cpf, context) =>
                {
                    var cpfPattern = "^(\\d{3}.\\d{3}.\\d{3}-\\d{2})|(\\d{11})$ ou ^\\d{3}\\x2E\\d{3}\\x2E\\d{3}\\x2D\\d{2}$";
                    var isMatch = Regex.IsMatch(cpf, cpfPattern);
                    if(!isMatch)
                    {
                        context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(cpf), ResourceErrorMessages.INVALID_CPF));
                    }

                });

            });

            When(r => !string.IsNullOrWhiteSpace(r.Password), () =>
            {
                RuleFor(r => r.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceErrorMessages.INVALID_PASSWORD);
            });

        }
    }
}
