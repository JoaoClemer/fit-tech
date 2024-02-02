using FitTech.Comunication.Requests.Login;
using FitTech.Comunication.Responses.Login;

namespace FitTech.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        Task<ResponseDoLoginDTO> Execute(RequestDoLoginDTO request);
    }
}
