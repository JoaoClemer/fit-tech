using FitTech.Comunication.Requests.Student;
using FitTech.Comunication.Responses;

namespace FitTech.Application.UseCases.Student.Create
{
    public interface ICreateStudentUseCase
    {
        Task<ResponseCreateStudentDTO> Execute(RequestCreateStudentDTO request);
    }
}
