using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1.Student.Create
{
    public class CreateStudentApiTest : ControllerBase
    {
        private const string METODH = "student";
        public CreateStudentApiTest(FitTechWebApplicationFactory<Program> factory) : base(factory)
        {
            
        }

        [Fact]
        public async Task Valid_Success()
        {
            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.GymId = 1;

            var response = await PostRequest(METODH, request);

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
            var request = RequestCreateStudentBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.GymId = 0;

            var response = await PostRequest(METODH, request);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);
            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            errors.Should().HaveCount(2)
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.EMPTY_GYM_ID))
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.GYM_NOT_FOUND));
        }
    }
}
