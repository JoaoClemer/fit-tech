using FitTech.Comunication.Responses.Student;

namespace FitTech.Application.UseCases.Student.GetStudentById
{
    public interface IGetStudentByIdUseCase
    {
        Task<ResponseStudentInformationsDTO> Execute(int studentId);
    }
}
