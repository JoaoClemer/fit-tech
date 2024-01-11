using FitTech.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
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

        protected async Task<HttpResponseMessage> PostRequest(string metodh, object body)
        {
            var jsonString = JsonConvert.SerializeObject(body);

            return await _httpClient.PostAsync(metodh, new StringContent(jsonString, Encoding.UTF8, "application/json"));
        }
    }
}
