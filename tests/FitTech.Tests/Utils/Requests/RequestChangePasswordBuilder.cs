using Bogus;
using FitTech.Comunication.Requests.Login;

namespace FitTech.Tests.Utils.Requests
{
    public static class RequestChangePasswordBuilder
    {
        public static RequestChangePasswordDTO Build(int passwordLenght = 6)
        {
            return new Faker<RequestChangePasswordDTO>()
                .RuleFor(l => l.CurrentPassword, f => f.Internet.Password(passwordLenght))
                .RuleFor(l => l.NewPassword, f => f.Internet.Password(passwordLenght));
        }
    }
}
