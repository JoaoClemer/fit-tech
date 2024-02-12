using FitTech.Comunication.Requests.Gym;
using FitTech.Comunication.Responses.Gym;

namespace FitTech.Application.UseCases.Gym.Create
{
    public interface ICreateGymUseCase
    {
        Task<ResponseCreateGymDTO> Execute(RequestCreateGymDTO request);
    }
}
