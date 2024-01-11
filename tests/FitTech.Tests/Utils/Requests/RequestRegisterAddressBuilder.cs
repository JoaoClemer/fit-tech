using Bogus;
using FitTech.Comunication.Requests.Address;

namespace FitTech.Tests.Utils.Requests
{
    public static class RequestRegisterAddressBuilder
    {
        public static RequestRegisterAddressDTO Build()
        {
            return new Faker<RequestRegisterAddressDTO>()
                .RuleFor(g => g.Street, f => f.Address.StreetName())
                .RuleFor(g => g.Number, f => f.Address.BuildingNumber())
                .RuleFor(g => g.Country, f => f.Address.Country())
                .RuleFor(g => g.City, f => f.Address.City())
                .RuleFor(g => g.State, f => f.Address.StateAbbr())
                .RuleFor(g => g.PostalCode, f => f.Address.ZipCode("#####-###"));
        }
    }
}
