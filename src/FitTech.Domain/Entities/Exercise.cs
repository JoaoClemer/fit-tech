namespace FitTech.Domain.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public int Reps { get; set; }
        public string Description { get; set; }
        public DayOfWeek DayOfTheWeek { get; set; }
        public List<Traning> Traning { get; set; }
    }
}
