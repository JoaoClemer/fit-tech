using Bogus;
using Bogus.Extensions.Brazil;
using FitTech.Comunication.Enum;
using FitTech.Comunication.Requests.Employee;
using FluentAssertions;

namespace FitTech.Tests.Utils.Requests
{
    public static class RequestCreateEmployeeBuilder
    {
        public static RequestCreateEmployeeDTO Build(int passwordLenght = 6)
        {
            return new Faker<RequestCreateEmployeeDTO>()
                .RuleFor(e => e.Name, f => f.Person.FullName)
                .RuleFor(e => e.Cpf, f => f.Person.Cpf(true))
                .RuleFor(e => e.EmailAddress, f => f.Internet.Email())
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"))
                .RuleFor(e => e.Password, f => f.Internet.Password(passwordLenght))
                .RuleFor(e => e.Salary, f => f.Random.Decimal(2000,3000))
                .RuleFor(e => e.EmployeeType, f => f.Random.Enum<EmployeeTypeDTO>())
                .RuleFor(e => e.GymId, f => f.Random.Int(1,30));
                
        }
    }
}
