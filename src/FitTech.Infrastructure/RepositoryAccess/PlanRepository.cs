﻿using FitTech.Domain.Entities;
using FitTech.Domain.Repositories.Plan;
using FitTech.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FitTech.Infrastructure.RepositoryAccess
{
    public class PlanRepository : IPlanWriteOnlyRepository, IPlanReadOnlyRepository
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

        public Task<bool> PlanNameIsInUse(string planName, int gymId)
        {
            return _context.Plans.AnyAsync(p => p.Name.ToUpper().Equals(planName.ToUpper()) && p.Gym.Id == gymId );
        }
    }
}
