using FitTech.Application.UseCases.Dashboard.GetStudentDashboard;
using FitTech.Comunication.Requests.Shared;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Repositories.Student;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Dashboard.GetStudentDashBoard
{
    public class GetStudentDashboardUseCaseTests
    {
        [Fact]
        public async Task Valid_Success()
        {

            var useCase = CreateUseCase(1);

            var response = await useCase.Execute();

            response.Should().NotBeNull();
            response.Results.ElementAt(0).Title.Should().Be("Alunos ativos");
            response.Results.ElementAt(0).Value.Should().Be("1");
            response.Results.ElementAt(1).Title.Should().Be("Alunos inativos");
            response.Results.ElementAt(1).Value.Should().Be("1");
        }

        private GetStudentDashboardUseCase CreateUseCase(int gymId = 0)
        {
            var readOnlyRepository = StudentReadOnlyRepositoryBuilder.Instance().GetAllStudentsOfGym(gymId).Build();
            var loggedUser = LoggedUserBuilder.Instance().GetLoggedUser(employee: true, password: "123456").Build();

            return new GetStudentDashboardUseCase(readOnlyRepository, loggedUser);
        }
    }

}
