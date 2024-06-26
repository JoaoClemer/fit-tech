using FitTech.Application.UseCases.Gym.Create;
using FitTech.Comunication.Requests.Gym;
using FitTech.Comunication.Responses.Gym;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    public class GymController : ControllerBase
    {
       
        [HttpPost( ApiRoutes.Gym.CreateGym )]
        [ProducesResponseType(typeof(ResponseCreateGymDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateGym(
            [FromServices]ICreateGymUseCase useCase,
            [FromBody]RequestCreateGymDTO request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}