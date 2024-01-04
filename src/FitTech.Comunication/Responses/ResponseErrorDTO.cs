namespace FitTech.Comunication.Responses
{
    public class ResponseErrorDTO
    {
        public List<string> Messages { get; set; }

        public ResponseErrorDTO(string message)
        {
            Messages = new List<string>() { message };
            
        }

        public ResponseErrorDTO(List<string> messages)
        {
            Messages = messages;

        }
    }
}
