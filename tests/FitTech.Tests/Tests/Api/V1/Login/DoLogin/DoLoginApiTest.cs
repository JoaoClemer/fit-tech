using FitTech.Api;
using FitTech.Comunication.Requests.Login;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1.Login.DoLogin
{
    public class DoLoginApiTest : ControllerBase
    {
        private Domain.Entities.Student _student;
        private Domain.Entities.Employee _employee;

        public DoLoginApiTest(FitTechWebApplicationFactory<Program> factory) : base(factory)
        {
            _student = factory.GetStudent();
            _employee = factory.GetEmployee();
        }

        [Fact]
        public async Task Valid_Success_Student()
        {
            var request = new RequestDoLoginDTO
            {
                EmailAddress = _student.EmailAddress,
                Password = _student.Password,
                UserType = Comunication.Enum.UserTypeDTO.Student
            };
                     
            var response = await PostRequest(ApiRoutes.Login.DoLogin, request);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNull();
            responseData.RootElement.GetProperty("token").GetString().Should().NotBeNull();

        }

        [Fact]
        public async Task Valid_Success_Employee()
        {
            var request = new RequestDoLoginDTO
            {
                EmailAddress = _employee.EmailAddress,
                Password = _employee.Password,
                UserType = Comunication.Enum.UserTypeDTO.Employee
            };

            var response = await PostRequest(ApiRoutes.Login.DoLogin, request);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNull();
            responseData.RootElement.GetProperty("token").GetString().Should().NotBeNull();

        }

        [Fact]
        public async Task Valid_Error_Invalid_Password()
        {
            var request = new RequestDoLoginDTO
            {
                EmailAddress = _employee.EmailAddress,
                Password = "invalidpassword",
                UserType = Comunication.Enum.UserTypeDTO.Employee
            };

            var response = await PostRequest(ApiRoutes.Login.DoLogin, request);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            errors.Should().HaveCount(1)
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.INVALID_LOGIN));
      
        }
    
    }
}
