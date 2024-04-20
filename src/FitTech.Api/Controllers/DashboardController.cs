using FitTech.Application.UseCases.Dashboard.GetStudentDashboard;
using FitTech.Comunication.Responses.Shared;
using FitTech.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [Authorize(Roles = $"{nameof(EmployeeType.Administrator)},{nameof(EmployeeType.Teacher)}")]
        [HttpGet(ApiRoutes.Dashboard.GetStudentDashboard)]
        [ProducesResponseType(typeof(ResponseDashboardDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStudentsOfGym([FromServices] IGetStudentDashboardUseCase useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }
    }
}
