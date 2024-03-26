using FitTech.Application.UseCases.Student.Create;
using FitTech.Comunication.Requests.Student;
using FitTech.Comunication.Responses.Student;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {

        [HttpPost( ApiRoutes.Student.CreateStudent )]
        [ProducesResponseType(typeof(ResponseCreateStudentDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateStudent(
            [FromServices] ICreateStudentUseCase useCase,
            [FromBody] RequestCreateStudentDTO request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
