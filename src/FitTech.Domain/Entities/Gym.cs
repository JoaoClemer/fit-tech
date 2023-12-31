namespace FitTech.Domain.Entities
{
    public class Gym : BaseEntity
    {

        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public Adress Adress { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
