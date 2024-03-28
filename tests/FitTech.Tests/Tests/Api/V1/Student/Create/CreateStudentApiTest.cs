using FitTech.Api;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1.Student.Create
{
    public class CreateStudentApiTest : ControllerBase
    {
        private Domain.Entities.Employee _admEmployee;
        private Domain.Entities.Employee _basicEmployee;
        public CreateStudentApiTest(FitTechWebApplicationFactory<Program> factory) : base(factory)
        {
            _admEmployee = factory.GetAdmEmployee();
            _basicEmployee = factory.GetEmployee();
        }

        [Fact]
        public async Task Valid_Success()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.GymId = 1;

            var response = await PostRequest(ApiRoutes.Student.CreateStudent, request, token);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            responseData.RootElement.GetProperty("gymName").GetString().Should().NotBeNull();
            responseData.RootElement.GetProperty("emailAddress").GetString().Should().NotBeNull();
            responseData.RootElement.GetProperty("token").GetString().Should().NotBeNull();

        }

        [Fact]
        public async Task Valid_Error_Invalid_Gym()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.GymId = 0;

            var response = await PostRequest(ApiRoutes.Student.CreateStudent, request, token);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);
            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            errors.Should().HaveCount(2)
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.EMPTY_GYM_ID))
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.GYM_NOT_FOUND));
        }

        [Fact]
        public async Task Valid_Error_Not_An_Administrator()
        {
            var token = await Login(_basicEmployee.EmailAddress, _basicEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.GymId = 1;

            var response = await PostRequest(ApiRoutes.Student.CreateStudent, request, token);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);

        }
    }
}
