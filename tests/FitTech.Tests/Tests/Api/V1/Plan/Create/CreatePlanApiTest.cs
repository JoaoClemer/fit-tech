using FitTech.Api;
using FitTech.Exceptions;
using FitTech.Tests.Utils.Requests;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1.Plan.Create
{
    public class CreatePlanApiTest : ControllerBase
    {      
        private Domain.Entities.Employee _admEmployee;
        private Domain.Entities.Employee _basicEmployee;
        private Domain.Entities.Plan _plan;
        public CreatePlanApiTest(FitTechWebApplicationFactory<Program> factory) : base(factory)
        {
            _admEmployee = factory.GetAdmEmployee();
            _basicEmployee = factory.GetEmployee();
            _plan = factory.GetPlan();

        }

        [Fact]
        public async Task Valid_Success()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = RequestCreatePlanBuilder.Build();
            request.GymId = 1;

            var response = await PostRequest(ApiRoutes.Plan.CreatePlan, request, token);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            responseData.RootElement.GetProperty("name").GetString().Should().NotBeNull();
            responseData.RootElement.GetProperty("price").GetDecimal().Should().NotBe(0);
            responseData.RootElement.GetProperty("planType").GetString().Should().NotBeNull();

        }

        [Fact]
        public async Task Valid_Error_Not_An_Administrator()
        {
            var token = await Login(_basicEmployee.EmailAddress, _basicEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = RequestCreatePlanBuilder.Build();
            request.GymId = 1;

            var response = await PostRequest(ApiRoutes.Plan.CreatePlan, request, token);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);

        }

        [Fact]
        public async Task Valid_Error_Invalid_Gym()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = RequestCreatePlanBuilder.Build();
            request.GymId = 2221;

            var response = await PostRequest(ApiRoutes.Plan.CreatePlan, request, token);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            errors.Should().HaveCount(1)
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.GYM_NOT_FOUND));
        }

        [Fact]
        public async Task Valid_Error_Name_In_Use()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var request = RequestCreatePlanBuilder.Build();
            request.GymId = 1;
            request.Name = _plan.Name;

            var response = await PostRequest(ApiRoutes.Plan.CreatePlan, request, token);

            await using var bodyResponse = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(bodyResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();

            errors.Should().HaveCount(1)
                .And.Contain(e => e.GetString().Equals(ResourceErrorMessages.PLAN_NAME_IN_USE));
        }
    }
}
