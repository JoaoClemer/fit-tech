using FitTech.Comunication.Requests.Shared;
using FitTech.Comunication.Responses.Student;

namespace FitTech.Application.UseCases.Student.GetAllStudentsOfGym
{
    public interface IGetAllStudentsOfGym
    {
        Task<ResponseGetAllStudentsOfGymDTO> Execute(RequestFilterDTO filter);
    }
}
