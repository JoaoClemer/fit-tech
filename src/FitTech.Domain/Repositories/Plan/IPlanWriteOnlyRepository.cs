namespace FitTech.Domain.Repositories.Plan
{
    public interface IPlanWriteOnlyRepository
    {
        Task CreatePlan(Entities.Plan plan);
    }
}
