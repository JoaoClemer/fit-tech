using FitTech.Domain.Enum;

namespace FitTech.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public Gym Gym { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PlanType PlanType { get; set; }
    }
}
