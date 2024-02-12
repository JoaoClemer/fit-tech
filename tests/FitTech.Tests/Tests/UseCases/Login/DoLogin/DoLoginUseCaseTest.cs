using FitTech.Application.UseCases.Login.DoLogin;
using FitTech.Domain.Entities;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Tests.Utils.Repositories.Employee;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Repositories.Student;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Valid_Success_Student()
        {
            var request = RequestDoLoginBuilder.Build();
            request.UserType = Comunication.Enum.UserTypeDTO.Student;

            var encryptyPassword = PasswordEncryptorBuilder.Instance().Encrypt(request.Password);
            var useCase = CreateUseCase(studentEmail: request.EmailAddress, studentPassword: encryptyPassword);

            var response = await useCase.Execute(request);

            response.Should().NotBeNull();
            response.Name.Should().NotBeNullOrWhiteSpace();
            response.Name.Should().Be("UseCaseTest");
            response.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Valid_Success_Employee()
        {
            var request = RequestDoLoginBuilder.Build();
            request.UserType = Comunication.Enum.UserTypeDTO.Employee;

            var encryptyPassword = PasswordEncryptorBuilder.Instance().Encrypt(request.Password);
            var useCase = CreateUseCase(employeeEmail: request.EmailAddress, employeePassword: encryptyPassword);

            var response = await useCase.Execute(request);

            response.Should().NotBeNull();
            response.Name.Should().NotBeNullOrWhiteSpace();
            response.Name.Should().Be("UseCaseTest");
            response.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Valid_Error_Invalid_Password_Student()
        {
            var request = RequestDoLoginBuilder.Build();
            request.UserType = Comunication.Enum.UserTypeDTO.Student;

            
            var useCase = CreateUseCase(studentEmail: request.EmailAddress, studentPassword: "invalidPassword");

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<InvalidLoginException>()
                .Where(ex => ex.Message.Contains(ResourceErrorMessages.INVALID_LOGIN));
        }

        [Fact]
        public async Task Valid_Error_Invalid_Password_Employee()
        {
            var request = RequestDoLoginBuilder.Build();
            request.UserType = Comunication.Enum.UserTypeDTO.Employee;


            var useCase = CreateUseCase(employeeEmail: request.EmailAddress, employeePassword: "invalidPassword");

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<InvalidLoginException>()
                .Where(ex => ex.Message.Contains(ResourceErrorMessages.INVALID_LOGIN));
        }

        [Fact]
        public async Task Valid_Error_Invalid_Email_Student()
        {
            var request = RequestDoLoginBuilder.Build();
            request.UserType = Comunication.Enum.UserTypeDTO.Student;

            var encryptyPassword = PasswordEncryptorBuilder.Instance().Encrypt(request.Password);
            var useCase = CreateUseCase(studentEmail: null, studentPassword: encryptyPassword);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<InvalidLoginException>()
                .Where(ex => ex.Message.Contains(ResourceErrorMessages.INVALID_LOGIN));
        }

        [Fact]
        public async Task Valid_Error_Invalid_Email_Employee()
        {
            var request = RequestDoLoginBuilder.Build();
            request.UserType = Comunication.Enum.UserTypeDTO.Employee;

            var encryptyPassword = PasswordEncryptorBuilder.Instance().Encrypt(request.Password);
            var useCase = CreateUseCase(employeeEmail: null, employeePassword: encryptyPassword);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<InvalidLoginException>()
                .Where(ex => ex.Message.Contains(ResourceErrorMessages.INVALID_LOGIN));
        }

        private DoLoginUseCase CreateUseCase(string studentEmail = null, string studentPassword = null, string employeeEmail = null, string employeePassword = null)
        {
            var studentReadOnlyRepository = StudentReadOnlyRepositoryBuilder.Instance().Login(studentEmail, studentPassword).Build();
            var employeeReadOnlyRepository = EmployeeReadOnlyRepositoryBuilder.Instance().Login(employeeEmail, employeePassword).Build();
            var passwordEncryptor = PasswordEncryptorBuilder.Instance();
            var tokenController = TokenControllerBuilder.Instance();

            return new DoLoginUseCase(studentReadOnlyRepository, employeeReadOnlyRepository, passwordEncryptor, tokenController);
        }
    }
}
