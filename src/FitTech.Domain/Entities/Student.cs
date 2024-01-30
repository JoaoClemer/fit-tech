namespace FitTech.Domain.Entities
{
    public class Student : User
    {
        public int RegistrationNumber { get; set; }
        public Plan Plan { get; set; }
        public Traning? Traning { get; set; }
    }
}
