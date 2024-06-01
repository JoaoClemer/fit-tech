using FitTech.Comunication.Responses.Shared;

namespace FitTech.Comunication.Responses.Student
{
    public class ResponseStudentInformationsDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public ResponseAddressDTO Address { get; set; }
        public int RegistrationNumber { get; set; }
    }
}
