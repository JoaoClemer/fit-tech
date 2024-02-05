using Bogus;
using FitTech.Comunication.Enum;
using FitTech.Comunication.Requests.Login;

namespace FitTech.Tests.Utils.Requests
{
    public static class RequestDoLoginBuilder
    {
        public static RequestDoLoginDTO Build(int passwordLenght = 6)
        {
            return new Faker<RequestDoLoginDTO>()
                .RuleFor(l => l.EmailAddress, f => f.Internet.Email())
                .RuleFor(l => l.Password, f => f.Internet.Password(passwordLenght))
                .RuleFor(l => l.UserType, f => f.Random.Enum<UserTypeDTO>());
        }
    }
}
