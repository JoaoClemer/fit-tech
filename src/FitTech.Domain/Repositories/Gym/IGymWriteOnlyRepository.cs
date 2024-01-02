namespace FitTech.Domain.Repositories.Gym
{
    public interface IGymWriteOnlyRepository
    {
        Task CreateGym(Entities.Gym gym);
    }
}
