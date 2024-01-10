using FitTech.Application.UseCases.Gym.Create;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Tests.Utils.Mapper;
using FitTech.Tests.Utils.Repositories;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Gym.Create
{
    public class CreateGymUseCaseTest
    {
        [Fact]
        public async Task Valid_Success()
        {
            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase();

            var response = await useCase.Execute(request);

            response.Should().NotBeNull();
            response.Name.Should().NotBeNullOrWhiteSpace();
            response.EmailAddress.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Valid_Error_Email_In_Use()
        {
            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase(request.EmailAddress, null);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.GYM_EMAIL_IN_USE));

        }

        [Fact]
        public async Task Valid_Error_Name_In_Use()
        {
            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var useCase = CreateUseCase(null, request.Name);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.GYM_NAME_IN_USE));

        }

        private CreateGymUseCase CreateUseCase(string email = null, string name = null)
        {
            var writeOnlyRepository = GymWriteOnlyRepositoryBuilder.Instance().Build();
            var readOnlyRepository = GymReadOnlyRepositoryBuilder.Instance().GetGymByEmail(email).GetGymByName(name).Build();
            var mapper = MapperBuilder.Instance();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();

            return new CreateGymUseCase(writeOnlyRepository, readOnlyRepository, mapper, unitOfWork);
        }
    }
}
