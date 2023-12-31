namespace FitTech.Domain.Entities
{
    public abstract class User : BaseEntity
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public Adress Adress { get; set; }
    }
}
