using FitTech.Domain.Entities;

namespace FitTech.Domain.Repositories.Gym
{
    public interface IGymReadOnlyRepository
    {
        Task<Entities.Gym?> GetGymByEmail(string email);
        Task<Entities.Gym?> GetGymById(int id);
    }
}
