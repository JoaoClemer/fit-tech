using FitTech.Application.UseCases.Employee.Create;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Tests.Utils.Mapper;
using FitTech.Tests.Utils.Repositories.Employee;
using FitTech.Tests.Utils.Repositories.Gym;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Employee.Create
{
    public class CreateEmployeeUseCaseTest
    {
        [Fact]
        public async Task Valid_Success()
        {
            var request = RequestCreateEmployeeBuilder.Build();
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
            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase();

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.GYM_NOT_FOUND));

        }

        [Fact]
        public async Task Valid_Error_Email_In_Use()
        {
            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase(request.GymId, request.EmailAddress);
        
            Func<Task> action = async () => { await useCase.Execute(request); };
        
            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.EMPLOYEE_EMAIL_IN_USE));
        
        }
        
        [Fact]
        public async Task Valid_Error_CPF_In_Use()
        {
            var request = RequestCreateEmployeeBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase(request.GymId, employeeCPF: request.Cpf);
        
            Func<Task> action = async () => { await useCase.Execute(request); };
        
            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.EMPLOYEE_CPF_IN_USE));
        
        }

        private CreateEmployeeUseCase CreateUseCase(int gymId = 0, string employeeEmail = null, string employeeCPF = null, int employeeId = 0)
        {
            var writeOnlyRepository = EmployeeWriteOnlyRepositoryBuilder.Instance().Build();
            var readOnlyRepository = EmployeeReadOnlyRepositoryBuilder.Instance().GetEmployeeByEmail(employeeEmail).GetEmployeeByCPF(employeeCPF).GetEmployeeById(employeeId).Build();
            var gymReadOnlyRepository = GymReadOnlyRepositoryBuilder.Instance().GetGymById(gymId).Build();
            var passwordEncryptor = PasswordEncryptorBuilder.Instance();
            var tokenController = TokenControllerBuilder.Instance();
            var mapper = MapperBuilder.Instance();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();

            return new CreateEmployeeUseCase(writeOnlyRepository, readOnlyRepository, mapper, unitOfWork, gymReadOnlyRepository, passwordEncryptor, tokenController);
        }
    }
}
