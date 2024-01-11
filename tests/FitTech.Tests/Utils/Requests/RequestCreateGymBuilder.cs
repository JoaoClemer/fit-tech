using Bogus;
using FitTech.Comunication.Requests.Gym;

namespace FitTech.Tests.Utils.Requests
{
    public static class RequestCreateGymBuilder
    {
        public static RequestCreateGymDTO Build()
        {
            return new Faker<RequestCreateGymDTO>()
                .RuleFor(g => g.Name, f => f.Company.CompanyName())
                .RuleFor(g => g.EmailAddress, f => f.Internet.Email())
                .RuleFor(g => g.PhoneNumber, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));
        }
    }
}
