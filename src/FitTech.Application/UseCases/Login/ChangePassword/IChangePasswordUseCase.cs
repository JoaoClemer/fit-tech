using FitTech.Comunication.Requests.Login;

namespace FitTech.Application.UseCases.Login.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordDTO request);
    }
}
