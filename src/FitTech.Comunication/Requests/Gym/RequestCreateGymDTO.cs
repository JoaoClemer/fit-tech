using FitTech.Comunication.Requests.Address;

namespace FitTech.Comunication.Requests.Gym
{
    public class RequestCreateGymDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public RequestRegisterAddressDTO Address { get; set; }
    }
}
