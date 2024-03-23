using FitTech.Application.UseCases.Plan.Create;
using FitTech.Exceptions;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Tests.Utils.Mapper;
using FitTech.Tests.Utils.Repositories.Gym;
using FitTech.Tests.Utils.Repositories.Plan;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Plan.Create
{
    public class CreatePlanUseCaseTest
    {
        [Fact]
        public async Task Valid_Success()
        {
            var request = RequestCreatePlanBuilder.Build();

            var useCase = CreateUseCase(request.GymId);

            var response = await useCase.Execute(request);

            response.Should().NotBeNull();
            response.Name.Should().NotBeNullOrWhiteSpace();
            response.Price.Should().NotBe(0);
            response.PlanType.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Valid_Error_Invalid_GymId()
        {
            var request = RequestCreatePlanBuilder.Build();

            var useCase = CreateUseCase();

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.GYM_NOT_FOUND));
        }

        [Fact]
        public async Task Valid_Error_Name_In_Use()
        {
            var request = RequestCreatePlanBuilder.Build();

            var useCase = CreateUseCase(request.GymId, request.Name);

            Func<Task> action = async () => { await useCase.Execute(request); };

            await action.Should().ThrowAsync<ValidationErrorsException>()
                .Where(ex => ex.ErrorMessages.Count == 1 && ex.ErrorMessages.Contains(ResourceErrorMessages.PLAN_NAME_IN_USE));
        }


        private CreatePlanUseCase CreateUseCase(int gymId = 0, string name = null)
        {
            var gymReadOnlyRepository = GymReadOnlyRepositoryBuilder.Instance().GetGymById(gymId).Build();
            var planReadOnlyRepository = PlanReadOnlyRepositoryBuilder.Instance().PlanNameIsInUse(name, gymId).Build();
            var planWriteOnlyRepository = PlanWriteOnlyRepositoryBuilder.Instance().Build();
            var mapper = MapperBuilder.Instance();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();

            return new CreatePlanUseCase(gymReadOnlyRepository, planWriteOnlyRepository, planReadOnlyRepository, mapper, unitOfWork);
        }
    }
}
