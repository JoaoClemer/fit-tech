using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1.Gym.Create
{
    public class CreateGymApiTest : ControllerBase
    {
        private const string METODH = "gym";
        public CreateGymApiTest(FitTechWebApplicationFactory<Program> factory) : base(factory)
        {
            
        }

        [Fact]
        public async Task Valid_Success()
        {
            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();

            var response = await PostRequest(METODH, request);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);
            
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNull();
            responseData.RootElement.GetProperty("emailAddress").GetString().Should().NotBeNull();

        }
        [Fact]
        public async Task Valid_Error_Empty_Name()
        {
            var request = RequestCreateGymBuilder.Build();
            request.Address = RequestRegisterAddressBuilder.Build();
            request.Name = string.Empty;

            var response = await PostRequest(METODH, request);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);
            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            errors.Should().ContainSingle().And.Contain(e => e.GetString().Equals(ResourceErrorMessages.EMPTY_NAME));
        }
    }
}
