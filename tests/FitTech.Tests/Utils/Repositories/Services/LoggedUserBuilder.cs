using FitTech.Application.Services.LoggedUser;
using Moq;

namespace FitTech.Tests.Utils.Repositories.Services
{
    public class LoggedUserBuilder
    {
        private static LoggedUserBuilder _instance;
        private readonly Mock<ILoggedUser> _repository;

        private LoggedUserBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<ILoggedUser>();
            }
        }

        public static LoggedUserBuilder Instance()
        {
            _instance = new LoggedUserBuilder();
            return _instance;
        }

        public LoggedUserBuilder GetLoggedUser(bool student = false, bool employee = false, string password = "")
        {
            if (employee)
                _repository.Setup(i => i.GetLoggedUser()).ReturnsAsync(new Domain.Entities.Employee()
                {
                    Id = 1,
                    EmailAddress = "student@email.com",
                    Password = password
                });

            if (student)
                _repository.Setup(i => i.GetLoggedUser()).ReturnsAsync(new Domain.Entities.Student()
                {
                    Id = 1,
                    EmailAddress = "employee@email.com",
                    Password = password
                });

            return this;
        }

        public ILoggedUser Build()
        {
            return _repository.Object;
        }
    }
}
