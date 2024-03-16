using FitTech.Comunication.Requests.Plan;

namespace FitTech.Application.UseCases.Plan.Create
{
    public interface ICreatePlanUseCase
    {
        Task Execute(RequestCreatePlanDTO request);
    }
}
