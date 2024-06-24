using FitTech.Application.UseCases.Student.GetAllStudentsOfGym;
using FitTech.Application.UseCases.Student.GetStudentById;
using FitTech.Comunication.Requests.Shared;
using FitTech.Exceptions.ExceptionsBase;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Mapper;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Repositories.Student;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using Xunit;

namespace FitTech.Tests.Tests.UseCases.Student.GetStudentById
{
    public class GetStudentByIdUseCaseTest
    {
        [Fact]
        public async Task Valid_Success()
        {
            var useCase = CreateUseCase(1);

            var response = await useCase.Execute(1);

            response.Should().NotBeNull();
            response.Name.Should().Be("João");
            response.Cpf.Should().Be("111.111.111-11");
            response.Address.Should().NotBeNull();
            response.Address.State.Should().Be("SP");
            response.Address.City.Should().Be("São Paulo");
            response.Address.Street.Should().Be("Av test");
            response.Address.Number.Should().Be("1");
            response.Address.Country.Should().Be("Brasil");
            response.Address.PostalCode.Should().Be("1111-111");
        }

        [Fact]
        public async Task Valid_Error_Student_Not_Found()
        {
            var useCase = CreateUseCase();

            Func<Task> action = async () => { await useCase.Execute(0); };

            await action.Should().ThrowAsync<FitTechException>()
                .Where(ex => ex.Message == ResourceErrorMessages.STUDENT_NOT_FOUND);

        }

        private GetStudentByIdUseCase CreateUseCase(int studentId = 0)
        {
            var readOnlyRepository = StudentReadOnlyRepositoryBuilder.Instance().GetStudentById(studentId).Build();
            var loggedUser = LoggedUserBuilder.Instance().GetLoggedUser(employee: true, password: "123456").Build();
            var mapper = MapperBuilder.Instance();

            return new GetStudentByIdUseCase(readOnlyRepository, mapper);
        }
    }
}
