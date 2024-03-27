namespace FitTech.Comunication.Requests.Shared
{
    public class RequestFilterDTO
    {
        public string FilterText { get; set; }

        public bool OnlyIsActive { get; set; }

        public bool OnlyIsNotActive { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageCount { get; set; } = 10;
    }
}
