using FitTech.Comunication.Requests.Employee;
using FitTech.Comunication.Responses.Employee;

namespace FitTech.Application.UseCases.Employee.Create
{
    public interface ICreateEmployeeUseCase
    {
        Task<ResponseCreateEmployeeDTO> Execute(RequestCreateEmployeeDTO request);
    }
}
