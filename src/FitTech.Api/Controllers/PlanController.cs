using FitTech.Api.Filters;
using FitTech.Application.UseCases.Plan.Create;
using FitTech.Comunication.Requests.Plan;
using FitTech.Comunication.Responses.Employee;
using FitTech.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(AuthUserFilter))]
    public class PlanController : ControllerBase
    {
        [Authorize(Roles = nameof(EmployeeType.Administrator))]
        [HttpPost( ApiRoutes.Plan.CreatePlan )]
        [ProducesResponseType( StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePlan(
            [FromServices] ICreatePlanUseCase useCase,
            [FromBody] RequestCreatePlanDTO request
            )
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response );
        }
    }
}
