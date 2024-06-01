using FitTech.Application.UseCases.Student.Create;
using FitTech.Application.UseCases.Student.GetAllStudentsOfGym;
using FitTech.Application.UseCases.Student.GetStudentById;
using FitTech.Comunication.Requests.Shared;
using FitTech.Comunication.Requests.Student;
using FitTech.Comunication.Responses.Shared;
using FitTech.Comunication.Responses.Student;
using FitTech.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {

        [Authorize(Roles = nameof(EmployeeType.Administrator))]
        [HttpPost( ApiRoutes.Student.CreateStudent )]
        [ProducesResponseType(typeof(ResponseCreateStudentDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateStudent(
            [FromServices] ICreateStudentUseCase useCase,
            [FromBody] RequestCreateStudentDTO request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [Authorize(Roles = $"{nameof(EmployeeType.Administrator)},{nameof(EmployeeType.Teacher)}")]
        [HttpGet( ApiRoutes.Student.GetAllStudentsOfGym )]
        [ProducesResponseType(typeof(ResponseListForTableDTO<ResponseStudentInListDTO>), StatusCodes.Status200OK)]        
        public async Task<IActionResult> GetAllStudentsOfGym(
            [FromServices] IGetAllStudentsOfGymUseCase useCase,
            [FromQuery] RequestFilterDTO filter)
        {
            var result = await useCase.Execute(filter);

            return Ok(result);
        }

        [Authorize(Roles = $"{nameof(EmployeeType.Administrator)},{nameof(EmployeeType.Teacher)}")]
        [HttpGet(ApiRoutes.Student.GetStudentById)]
        [ProducesResponseType(typeof(ResponseStudentInformationsDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult>GetStudentById(
            [FromRoute] int studentId,
            [FromServices] IGetStudentByIdUseCase useCase)
        {
            var result = await useCase.Execute(studentId);

            return Ok(result);
        }

    }
}
