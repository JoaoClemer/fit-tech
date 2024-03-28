using FitTech.Api;
using FitTech.Comunication.Enum;
using FitTech.Comunication.Requests.Login;
using FitTech.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Xunit;

namespace FitTech.Tests.Tests.Api.V1
{
    public class ControllerBase : IClassFixture<FitTechWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ControllerBase(FitTechWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            ResourceErrorMessages.Culture = CultureInfo.CurrentCulture;
        }

        protected async Task<HttpResponseMessage> PostRequest(string metodh, object body, string token = "")
        {
            AuthorizeRequest(token);
            var jsonString = JsonConvert.SerializeObject(body);

            return await _httpClient.PostAsync(metodh, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }

        protected async Task<HttpResponseMessage> PutRequest(string metodo, object body, string token = "")
        {
            AuthorizeRequest(token);
            var jsonString = JsonConvert.SerializeObject(body);

            return await _httpClient.PutAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }

        protected async Task<string> Login(string email, string password, UserTypeDTO userType)
        {
            var request = new RequestDoLoginDTO
            {
                EmailAddress = email,
                UserType = userType,
                Password = password
            };

            var response = await PostRequest(ApiRoutes.Login.DoLogin, request);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            return responseData.RootElement.GetProperty("token").GetString();

        }
        private void AuthorizeRequest(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }
    }
}
