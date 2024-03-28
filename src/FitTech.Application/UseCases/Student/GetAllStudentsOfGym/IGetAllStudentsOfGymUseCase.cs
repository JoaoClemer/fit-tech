using FitTech.Comunication.Requests.Shared;
using FitTech.Comunication.Responses.Shared;
using FitTech.Comunication.Responses.Student;

namespace FitTech.Application.UseCases.Student.GetAllStudentsOfGym
{
    public interface IGetAllStudentsOfGymUseCase
    {
        Task<ResponseListForTableDTO<ResponseStudentInListDTO>> Execute(RequestFilterDTO filter);
    }
}
