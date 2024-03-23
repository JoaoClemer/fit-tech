using Bogus;
using FitTech.Domain.Entities;
using FitTech.Domain.Enum;

namespace FitTech.Tests.Utils.SeedDataFactory
{
    public static class PlanSeedDataFactory
    {
        public static Plan BuildPlan()
        {            
            return new Faker<Plan>()
                .RuleFor(e => e.Name, f => f.Person.FullName)
                .RuleFor(e => e.PlanType, f => f.Random.Enum<PlanType>(0))
                .RuleFor(e => e.Price, f => f.Random.Decimal(50, 500));
        }
    }
}
