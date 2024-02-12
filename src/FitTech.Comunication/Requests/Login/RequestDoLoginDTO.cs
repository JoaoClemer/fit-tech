using FitTech.Comunication.Enum;

namespace FitTech.Comunication.Requests.Login
{
    public class RequestDoLoginDTO
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public UserTypeDTO UserType { get; set; }
    }
}
