using FitTech.Comunication.Requests.Plan;
using FitTech.Comunication.Responses.Plan;

namespace FitTech.Application.UseCases.Plan.Create
{
    public interface ICreatePlanUseCase
    {
        Task<ResponseCreatePlanDTO> Execute(RequestCreatePlanDTO request);
    }
}
