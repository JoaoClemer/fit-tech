namespace FitTech.Domain.Repositories.Plan
{
    public interface IPlanReadOnlyRepository
    {
        Task<bool> PlanNameIsInUse(string planName, int gymId);
    }
}
