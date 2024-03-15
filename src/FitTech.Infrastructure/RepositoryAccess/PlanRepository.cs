using FitTech.Domain.Entities;
using FitTech.Domain.Repositories.Plan;
using FitTech.Infrastructure.Context;

namespace FitTech.Infrastructure.RepositoryAccess
{
    public class PlanRepository : IPlanWriteOnlyRepository
    {
        private readonly FitTechContext _context;
        public PlanRepository(FitTechContext context)
        {
            _context = context;
        }
        public async Task CreatePlan(Plan plan)
        {
            await _context.Plans.AddAsync(plan);
        }
    }
}
