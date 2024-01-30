using FitTech.Application.UseCases.Student.Create;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Mapper;
using FitTech.Tests.Utils.Repositories.Gym;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Repositories.Student;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Student
{
    public class CreateStudentUseCaseTest
    {
        [Fact]
        public async Task Valid_Success()
        {
            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase(request.GymId);

            var response = await useCase.Execute(request);

            response.Should().NotBeNull();
            response.Token.Should().NotBeNullOrWhiteSpace();
            response.EmailAddress.Should().NotBeNullOrWhiteSpace();
            response.GymName.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Valid_Error_Invalid_GymId()
        {
            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase();

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.GYM_NOT_FOUND));

        }

        [Fact]
        public async Task Valid_Error_Email_In_Use()
        {
            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase(request.GymId, request.EmailAddress);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.STUDENT_EMAIL_IN_USE));

        }

        [Fact]
        public async Task Valid_Error_CPF_In_Use()
        {
            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase(request.GymId, studentCPF: request.Cpf);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.STUDENT_CPF_IN_USE));

        }

        private CreateStudentUseCase CreateUseCase(int gymId = 0, string studentEmail = null, string studentCPF = null, int studentId = 0)
        {
            var writeOnlyRepository = StudentWriteOnlyRepositoryBuilder.Instance().Build();
            var readOnlyRepository = StudentReadOnlyRepositoryBuilder.Instance().GetStudentByEmail(studentEmail).GetStudentByCPF(studentCPF).GetStudentById(studentId).IsRegisterNumberUnique(0).Build();
            var gymReadOnlyRepository = GymReadOnlyRepositoryBuilder.Instance().GetGymById(gymId).Build();
            var passwordEncryptor = PasswordEncryptorBuilder.Instance();
            var tokenController = TokenControllerBuilder.Instance();
            var mapper = MapperBuilder.Instance();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();

            return new CreateStudentUseCase(readOnlyRepository, writeOnlyRepository, gymReadOnlyRepository, mapper, unitOfWork, passwordEncryptor, tokenController);
        }
    }
}
