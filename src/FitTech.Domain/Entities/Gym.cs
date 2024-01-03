namespace FitTech.Domain.Entities
{
    public class Gym : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
