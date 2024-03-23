using Bogus;
using FitTech.Comunication.Enum;
using FitTech.Comunication.Requests.Plan;

namespace FitTech.Tests.Utils.Requests
{
    public class RequestCreatePlanBuilder
    {
        public static RequestCreatePlanDTO Build()
        {
            return new Faker<RequestCreatePlanDTO>()
                .RuleFor(e => e.Name, f => f.Commerce.ProductName())
                .RuleFor(e => e.Price, f => f.Random.Decimal(90, 500))
                .RuleFor(e => e.PlanType, f => f.Random.Enum<PlanTypeDTO>(0))
                .RuleFor(e => e.GymId, f => f.Random.Int(1, 30));
        }
    }
}
