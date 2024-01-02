namespace FitTech.Domain.Entities
{
    public class Traning : BaseEntity
    {
        public List<Exercise> Exercises { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
    }
}