using Bogus;
using FitTech.Domain.Entities;

namespace FitTech.Tests.Utils.SeedDataFactory
{
    public static class GymSeedDataFactory
    {

        public static Gym BuildSimpleGym()
        {
            var fakeAddress = new Faker<Address>()
                .RuleFor(a => a.Street, f => f.Address.StreetName())
                .RuleFor(a => a.Number, f => f.Address.BuildingNumber())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.StateAbbr())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode("#####-###"));

            return new Faker<Gym>()
                .RuleFor(g => g.Name, f => f.Company.CompanyName())
                .RuleFor(g => g.EmailAddress, f => f.Internet.Email())
                .RuleFor(g => g.PhoneNumber, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"))
                .RuleFor(g => g.Address, () => fakeAddress);
                
        }
    }
}
