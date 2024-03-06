using FitTech.Domain.Entities;

namespace FitTech.Application.Services.LoggedUser
{
    public interface ILoggedUser
    {
        Task<User> GetLoggedUser();
    }
}
