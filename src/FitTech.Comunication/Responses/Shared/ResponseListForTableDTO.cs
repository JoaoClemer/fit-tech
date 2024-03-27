namespace FitTech.Comunication.Responses.Shared
{
    public class ResponseListForTableDTO<T>
    {
        public ResponseListForTableDTO( ICollection<T> entity, int currentPage, int pageSize, int pageCount)
        {
            Data = entity;
            CurrentPage = currentPage;
            PageSize = pageSize;
            PageCount = pageCount;
        }

        public ICollection<T> Data { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }
    }
}
