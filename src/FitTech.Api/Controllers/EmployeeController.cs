﻿using FitTech.Application.UseCases.Employee.Create;
using FitTech.Comunication.Requests.Employee;
using FitTech.Comunication.Responses.Employee;
using Microsoft.AspNetCore.Mvc;

namespace FitTech.Api.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpPost( ApiRoutes.Employee.CreateEmployee )]
        [ProducesResponseType(typeof(ResponseCreateEmployeeDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEmployee(
            [FromServices] ICreateEmployeeUseCase useCase,
            [FromBody]RequestCreateEmployeeDTO request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
