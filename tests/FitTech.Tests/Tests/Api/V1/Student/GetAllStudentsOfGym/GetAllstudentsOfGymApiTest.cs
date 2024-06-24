using FitTech.Api;
using FitTech.Comunication.Responses.Shared;
using FitTech.Comunication.Responses.Student;
using FluentAssertions;
using Newtonsoft.Json;
using System.Web;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1.Student.GetAllStudentsOfGym
{
    public class GetAllstudentsOfGymApiTest : ControllerBase
    {
        private Domain.Entities.Employee _admEmployee;
        private Domain.Entities.Student _student;
        public GetAllstudentsOfGymApiTest(FitTechWebApplicationFactory<Program> factory) : base(factory)
        {
            _admEmployee = factory.GetAdmEmployee();
            _student = factory.GetStudent();
        }

        [Fact]
        public async Task Valid_Success()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("pageNumber", "1");

            var response = await GetRequest(ApiRoutes.Student.GetAllStudentsOfGym, queryString.ToString(), token);

            var bodyResponse = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = JsonConvert.DeserializeObject<ResponseListForTableDTO<ResponseStudentInListDTO>>(bodyResponse);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNullOrEmpty().And.HaveCount(6);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(6);
            result.PageCount.Should().Be(2);

        }

        [Fact]
        public async Task Valid_Success_Only_Active()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("onlyIsActive", "true");

            var response = await GetRequest(ApiRoutes.Student.GetAllStudentsOfGym, queryString.ToString(), token);

            var bodyResponse = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = JsonConvert.DeserializeObject<ResponseListForTableDTO<ResponseStudentInListDTO>>(bodyResponse);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNullOrEmpty().And.HaveCount(6);
            result.Data.ToList().ForEach(s => s.PlanIsActive.Should().BeTrue());
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(6);
            result.PageCount.Should().Be(1);

        }

        [Fact]
        public async Task Valid_Success_Only_Not_Active()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("onlyIsNotActive", "true");

            var response = await GetRequest(ApiRoutes.Student.GetAllStudentsOfGym, queryString.ToString(), token);

            var bodyResponse = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = JsonConvert.DeserializeObject<ResponseListForTableDTO<ResponseStudentInListDTO>>(bodyResponse);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNullOrEmpty().And.HaveCount(5);
            result.Data.ToList().ForEach(s => s.PlanIsActive.Should().BeFalse());
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(6);
            result.PageCount.Should().Be(1);

        }

        [Fact]
        public async Task Valid_Success_Text_Filter()
        {
            var token = await Login(_admEmployee.EmailAddress, _admEmployee.Password, Comunication.Enum.UserTypeDTO.Employee);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("filterText", _student.Name);

            var response = await GetRequest(ApiRoutes.Student.GetAllStudentsOfGym, queryString.ToString(), token);

            var bodyResponse = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = JsonConvert.DeserializeObject<ResponseListForTableDTO<ResponseStudentInListDTO>>(bodyResponse);

            result.Should().NotBeNull();
            result.Data.Should().NotBeNullOrEmpty().And.HaveCount(1);
            result.Data.ToList().ForEach(s => s.Name.Should().Be(_student.Name));
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(6);
            result.PageCount.Should().Be(1);

        }

        [Fact]
        public async Task Valid_Error_Not_An_Employee()
        {
            var token = await Login(_student.EmailAddress, _student.Password, Comunication.Enum.UserTypeDTO.Student);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("filterText", _student.Name);

            var response = await GetRequest(ApiRoutes.Student.GetAllStudentsOfGym, queryString.ToString(), token);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);

        }
    }
}
