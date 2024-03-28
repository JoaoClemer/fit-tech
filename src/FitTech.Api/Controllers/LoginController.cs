using FitTech.Api.Filters;
using FitTech.Application.UseCases.Login.ChangePassword;
using FitTech.Application.UseCases.Login.DoLogin;
using FitTech.Comunication.Requests.Login;
using FitTech.Comunication.Responses.Login;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost( ApiRoutes.Login.DoLogin )]
        [ProducesResponseType(typeof(ResponseDoLoginDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromServices] IDoLoginUseCase useCase,
            [FromBody] RequestDoLoginDTO request)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpPut( ApiRoutes.Login.ChangePassword )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(AuthUserFilter))]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordDTO request)
        {
            await useCase.Execute(request);

            return NoContent();
        }
    }
}
