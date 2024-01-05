using FitTech.Application.UseCases.Gym.Create;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult> Get([FromServices]ICreateGymUseCase useCase)
        {
            await useCase.Execute(new Comunication.Requests.Gym.RequestCreateGymDTO
            {
                Name = "SmartTech",
                EmailAddress = "stmart@tech.com",
                PhoneNumber = "11 9 4963-2007",
                Address = new Comunication.Requests.Address.RequestRegisterAddressDTO
                {
                    City = "São Paulo",
                    Country = "Brasil",
                    State = "SP",
                    Number = "22",
                    PostalCode = "00000-000",
                    Street = "Rua Lurdes"
                }
            });

            return Ok();
        }
    }
}