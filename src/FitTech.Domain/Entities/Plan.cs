using FitTech.Domain.Enum;

namespace FitTech.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Price { get; set; }
        public PlanType PlanType { get; set; }
    }
}
