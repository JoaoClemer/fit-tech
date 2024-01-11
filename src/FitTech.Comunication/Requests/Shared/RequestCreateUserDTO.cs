using FitTech.Comunication.Requests.Address;

namespace FitTech.Comunication.Requests.Shared
{
    public abstract class RequestCreateUserDTO
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public RequestRegisterAddressDTO Address { get; set; }
        public int GymId { get; set; }
    }
}
