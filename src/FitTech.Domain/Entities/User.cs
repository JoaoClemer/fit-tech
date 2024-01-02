namespace FitTech.Domain.Entities
{
    public abstract class User : BaseEntity
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public Gym Gym { get; set; }
    }
}
