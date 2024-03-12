using FitTech.Application.UseCases.Login.ChangePassword;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Repositories.Employee;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Repositories.Student;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Login.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Valid_Success_Student()
        {
            var request = RequestChangePasswordBuilder.Build();

            var encryptyPassword = PasswordEncryptorBuilder.Instance().Encrypt(request.CurrentPassword);
            var useCase = CreateUseCase(studentPassword: encryptyPassword);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Valid_Success_Employee()
        {
            var request = RequestChangePasswordBuilder.Build();

            var encryptyPassword = PasswordEncryptorBuilder.Instance().Encrypt(request.CurrentPassword);
            var useCase = CreateUseCase(employeePassword: encryptyPassword);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Valid_Error_Invalid_Current_Password_Student()
        {
            var request = RequestChangePasswordBuilder.Build();

            var useCase = CreateUseCase(studentPassword: "123456");

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Contains(ResourceErrorMessages.INVALID_CURRENT_PASSWORD));
        }

        [Fact]
        public async Task Valid_Error_Invalid_Current_Password_Employee()
        {
            var request = RequestChangePasswordBuilder.Build();

            var useCase = CreateUseCase(employeePassword: "123456");

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Contains(ResourceErrorMessages.INVALID_CURRENT_PASSWORD));
        }

        private ChangePasswordUseCase CreateUseCase(string studentPassword = null, string employeePassword = null)
        {
            var studentId = 0;
            var isStudent = false;
            var employeeId = 0;
            var isEmployee = false;
            var password = "";

            if(studentPassword != null)
            {
                studentId = 1;
                isStudent = true;
                password = studentPassword;
            }

            if(employeePassword != null)
            {
                employeeId = 1;
                isEmployee = true;
                password = employeePassword;
            }

            var studentReadOnlyRepository = StudentReadOnlyRepositoryBuilder.Instance().GetStudentById(studentId).Build();
            var employeeReadOnlyRepository = EmployeeReadOnlyRepositoryBuilder.Instance().GetEmployeeById(employeeId).Build();
            var employeeUpdateOnlyRepository = EmployeeUpdateOnlyRepositoryBuilder.Instance().Build();
            var studentUpdateOnlyRepository = StudentUpdateOnlyRepositoryBuilder.Instance().Build();
            var loggedUser = LoggedUserBuilder.Instance().GetLoggedUser(student: isStudent, employee:isEmployee, password: password).Build();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var passwordEncryptor = PasswordEncryptorBuilder.Instance();

            return new ChangePasswordUseCase(employeeUpdateOnlyRepository, studentUpdateOnlyRepository, studentReadOnlyRepository, employeeReadOnlyRepository, loggedUser, unitOfWork, passwordEncryptor);
        }
    }
}
