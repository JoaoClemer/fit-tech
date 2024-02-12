using Bogus;
using Bogus.Extensions.Brazil;
using FitTech.Comunication.Requests.Student;

namespace FitTech.Tests.Utils.Requests
{
    public class RequestCreateStudentBuilder
    {
        public static RequestCreateStudentDTO Build(int passwordLenght = 6)
        {
            return new Faker<RequestCreateStudentDTO>()
                .RuleFor(e => e.Name, f => f.Person.FullName)
                .RuleFor(e => e.Cpf, f => f.Person.Cpf(true))
                .RuleFor(e => e.EmailAddress, f => f.Internet.Email())
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"))
                .RuleFor(e => e.Password, f => f.Internet.Password(passwordLenght))
                .RuleFor(e => e.GymId, f => f.Random.Int(1, 30));

        }
    }
}
