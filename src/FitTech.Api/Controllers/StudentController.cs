using FitTech.Application.UseCases.Student.Create;
using FitTech.Comunication.Requests.Student;
using FitTech.Comunication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        [HttpPost]
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
