using FitTech.Comunication.Requests.Login;
using FitTech.Domain.Entities;
using FitTech.Domain.Enum;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Repositories.Services;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1.Login.ChangePassword
{
    public class ChangePasswordApiTest : ControllerBase
    {
        private const string METODH = "login/change-password";
        private Domain.Entities.Student _student;
        private Domain.Entities.Employee _employee;

        public ChangePasswordApiTest(FitTechWebApplicationFactory<Program> factory) : base(factory)
        {
            _student = factory.GetStudent();
            _employee = factory.GetEmployee();
        }

        [Fact]
        public async Task Valid_Success_Student()
        {
            var token = await Login(_student.EmailAddress, _student.Password, Comunication.Enum.UserTypeDTO.Student);

            var request = new RequestChangePasswordDTO
            {
              CurrentPassword = _student.Password,
              NewPassword = "newStudentPassWord123"
            };

            var response = await PutRequest(METODH, request, token);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        }

        [Fact]
        public async Task Valid_Success_Employee()
        {
            var token = await Login(_employee.EmailAddress, _employee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = new RequestChangePasswordDTO
            {
                CurrentPassword = _employee.Password,
                NewPassword = "newEmployeePassWord123"
            };

            var response = await PutRequest(METODH, request, token);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        }

        [Fact]
        public async Task Valid_Error_Invalid_Current_Password_Student()
        {
            var token = await Login(_student.EmailAddress, _student.Password, Comunication.Enum.UserTypeDTO.Student);

            var request = new RequestChangePasswordDTO
            {
                CurrentPassword = "invalidPassword",
                NewPassword = "newStudentPassWord123"
            };

            var response = await PutRequest(METODH, request, token);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            errors.Should().HaveCount(1)
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.INVALID_CURRENT_PASSWORD));

        }

        [Fact]
        public async Task Valid_Error_Invalid_Current_Password_Employee()
        {
            var token = await Login(_employee.EmailAddress, _employee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = new RequestChangePasswordDTO
            {
                CurrentPassword = "invalidPassword",
                NewPassword = "newEmployeePassWord123"
            };

            var response = await PutRequest(METODH, request, token);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            errors.Should().HaveCount(1)
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.INVALID_CURRENT_PASSWORD));

        }

        [Fact]
        public async Task Valid_Invalid_Token()
        {
            var token = string.Empty;

            var request = RequestChangePasswordBuilder.Build();
            request.CurrentPassword = _student.Password;

            var response = await PutRequest(METODH, request, token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Valid_Error_ValidToken_With_InvalidUser()
        {
            var token = TokenControllerBuilder.Instance().GenerateToken("user@fake.com", UserType.Student.ToString());

            var request = RequestChangePasswordBuilder.Build();
           request.CurrentPassword = _employee.Password;

            var response = await PutRequest(METODH, request, token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Valid_Error_ExpiredToken()
        {
            var token = TokenControllerBuilder.InstanceExpiredToken().GenerateToken(_student.EmailAddress, UserType.Student.ToString());
            Thread.Sleep(1000);

            var request = RequestChangePasswordBuilder.Build();
            request.CurrentPassword = _student.Password;

            var response = await PutRequest(METODH, request, token);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
