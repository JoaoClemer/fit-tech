namespace FitTech.Comunication.Responses.Shared
{
    public class ResponseDashboardDTO
    {
        public string GymName { get; set; }
        public ICollection<ResponseInformationDTO> Results { get; set; }
    }
}
