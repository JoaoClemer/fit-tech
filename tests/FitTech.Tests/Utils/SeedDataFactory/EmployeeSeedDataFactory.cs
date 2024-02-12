using Bogus;
using Bogus.Extensions.Brazil;
using FitTech.Domain.Entities;
using FitTech.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTech.Tests.Utils.SeedDataFactory
{
    public static class EmployeeSeedDataFactory
    {
        public static Employee BuildSimpleEmployee()
        {
            var fakeAddress = new Faker<Address>()
                .RuleFor(a => a.Street, f => f.Address.StreetName())
                .RuleFor(a => a.Number, f => f.Address.BuildingNumber())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.StateAbbr())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode("#####-###"));

            return new Faker<Employee>()
                .RuleFor(e => e.Name, f => f.Person.FullName)
                .RuleFor(e => e.Cpf, f => f.Person.Cpf(true))
                .RuleFor(e => e.EmailAddress, f => f.Internet.Email())
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"))
                .RuleFor(c => c.Password, f => f.Internet.Password())
                .RuleFor(e => e.Address, () => fakeAddress)
                .RuleFor(e => e.EmployeeType, f => f.Random.Enum<EmployeeType>())
                .RuleFor(e => e.Salary, f => f.Random.Decimal(2000, 3000));
        }
    }
}
