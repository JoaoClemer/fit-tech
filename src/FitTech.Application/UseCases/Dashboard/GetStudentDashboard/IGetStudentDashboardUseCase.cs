using FitTech.Comunication.Responses.Shared;

namespace FitTech.Application.UseCases.Dashboard.GetStudentDashboard
{
    public interface IGetStudentDashboardUseCase
    {
        Task<ResponseDashboardDTO> Execute();
    }
}
