using FitTech.Api;
using FitTech.Comunication.Enum;
using FitTech.Comunication.Responses.Student;
using FitTech.Exceptions;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1.Student.GetStudentById
{
    public class GetStudentByIdApiTest : ControllerBase
    {
        private Domain.Entities.Employee _admEmployee;
        private Domain.Entities.Student _student;

        public GetStudentByIdApiTest(FitTechWebApplicationFactory<Program> factory) : base(factory)
        {
            _admEmployee = factory.GetAdmEmployee();
            _student = factory.GetStudent();
        }

        [Fact]
        public async Task Valid_Success()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, UserTypeDTO.Employee);

            var response = await GetRequest(ApiRoutes.Student.GetStudentById.Replace("{studentId}", "1"), token: token);

            var bodyResponse = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = JsonConvert.DeserializeObject<ResponseStudentInformationsDTO>(bodyResponse);

            result.Should().NotBeNull();
            result.Should().NotBeNull();
            result.Name.Should().NotBeNullOrEmpty();
            result.Cpf.Should().NotBeNullOrEmpty();
            result.Address.Should().NotBeNull();
            result.Address.State.Should().NotBeNullOrEmpty();
            result.Address.City.Should().NotBeNullOrEmpty();
            result.Address.Street.Should().NotBeNullOrEmpty();
            result.Address.Number.Should().NotBeNullOrEmpty();
            result.Address.Country.Should().NotBeNullOrEmpty();
            result.Address.PostalCode.Should().NotBeNullOrEmpty();

        }

        [Fact]
        public async Task Valid_Error_Student_Not_Found()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, UserTypeDTO.Employee);

            var response = await GetRequest(ApiRoutes.Student.GetStudentById.Replace("{studentId}", "123456"), token: token);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            errors.Should().HaveCount(1)
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.STUDENT_NOT_FOUND));
        }
    }
}
