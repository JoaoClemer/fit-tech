using FitTech.Application.UseCases.Student.GetAllStudentsOfGym;
using FitTech.Comunication.Requests.Shared;
using FitTech.Tests.Utils.Mapper;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Repositories.Student;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Student.GetAllStudentsOfGym
{
    public class GetAllStudentsOfGymUseCaseTest
    {
        [Fact]
        public async Task Valid_Success()
        {
            var filter = new RequestFilterDTO();

            var useCase = CreateUseCase(1);

            var response = await useCase.Execute(filter);

            response.Should().NotBeNull();
            response.Data.Should().NotBeNullOrEmpty().And.HaveCount(2);
            response.PageCount.Should().Be(1);
            response.PageSize.Should().Be(10);
            response.CurrentPage.Should().Be(1);
        }

        [Fact]
        public async Task Valid_Success_Only_Active()
        {
            var filter = new RequestFilterDTO();
            filter.OnlyIsActive = true;

            var useCase = CreateUseCase(1);

            var response = await useCase.Execute(filter);

            response.Should().NotBeNull();
            response.Data.Should().NotBeNullOrEmpty();
            response.Data.ToList().ForEach(d => d.PlanIsActive.Should().BeTrue());
            response.PageCount.Should().Be(1);
            response.PageSize.Should().Be(10);
            response.CurrentPage.Should().Be(1);
        }

        [Fact]
        public async Task Valid_Success_Only_Not_Active()
        {
            var filter = new RequestFilterDTO();
            filter.OnlyIsNotActive = true;

            var useCase = CreateUseCase(1);

            var response = await useCase.Execute(filter);

            response.Should().NotBeNull();
            response.Data.Should().NotBeNullOrEmpty();
            response.Data.ToList().ForEach(d => d.PlanIsActive.Should().BeFalse());
            response.PageCount.Should().Be(1);
            response.PageSize.Should().Be(10);
            response.CurrentPage.Should().Be(1);
        }

        [Fact]
        public async Task Valid_Success_Text_Filter()
        {
            var filter = new RequestFilterDTO();
            filter.FilterText = "Jo";

            var useCase = CreateUseCase(1);

            var response = await useCase.Execute(filter);

            response.Should().NotBeNull();
            response.Data.Should().NotBeNullOrEmpty();
            response.Data.ToList().ForEach(d => d.Name.Should().Be("João"));
            response.PageCount.Should().Be(1);
            response.PageSize.Should().Be(10);
            response.CurrentPage.Should().Be(1);
        }

        [Fact]
        public async Task Valid_Success_With_Invalid_Text_Filter_Should_Return_Empty()
        {
            var filter = new RequestFilterDTO();
            filter.FilterText = "Invalid";

            var useCase = CreateUseCase(1);

            var response = await useCase.Execute(filter);

            response.Should().NotBeNull();
            response.Data.Should().BeNullOrEmpty();
            response.PageCount.Should().Be(0);
            response.PageSize.Should().Be(10);
            response.CurrentPage.Should().Be(1);
        }
        private GetAllStudentsOfGymUseCase CreateUseCase(int gymId = 0)
        {
            var readOnlyRepository = StudentReadOnlyRepositoryBuilder.Instance().GetAllStudentsOfGym(gymId).Build();
            var loggedUser = LoggedUserBuilder.Instance().GetLoggedUser(employee: true, password: "123456").Build();
            var mapper = MapperBuilder.Instance();

            return new GetAllStudentsOfGymUseCase(readOnlyRepository, loggedUser, mapper);
        }
    }
}
