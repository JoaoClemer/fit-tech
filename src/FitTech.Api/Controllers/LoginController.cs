using FitTech.Application.UseCases.Login.DoLogin;
using FitTech.Comunication.Requests.Login;
using FitTech.Comunication.Responses.Login;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDoLoginDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromServices] IDoLoginUseCase useCase,
            [FromBody] RequestDoLoginDTO request)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }
    }
}
