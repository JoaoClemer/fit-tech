using Bogus;
using Bogus.Extensions.Brazil;
using FitTech.Domain.Entities;
using FitTech.Tests.Utils.Repositories.Services;

namespace FitTech.Tests.Utils.SeedDataFactory
{
    public static class StudentSeedDataFactory
    {
        public static Student BuildSimpleStudent()
        {
            var fakeAddress = new Faker<Address>()
                .RuleFor(a => a.Street, f => f.Address.StreetName())
                .RuleFor(a => a.Number, f => f.Address.BuildingNumber())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.StateAbbr())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode("#####-###"));

            return new Faker<Student>()
                .RuleFor(e => e.Name, f => f.Person.FullName)
                .RuleFor(e => e.Cpf, f => f.Person.Cpf(true))
                .RuleFor(e => e.EmailAddress, f => f.Internet.Email())
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"))
                .RuleFor(c => c.Password, f => f.Internet.Password())
                .RuleFor(e => e.Address, () => fakeAddress);
        }
    }
}
