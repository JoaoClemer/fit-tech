namespace FitTech.Domain.Entities
{
    public class StudentPlan : BaseEntity
    {
        public Plan Plan { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
