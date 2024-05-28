namespace Financial.Core.Requests
{
    public abstract class PagedRequest : Request
    {
        public int PageSize { get; set; } = Configuration.DefaultPageNumber;
        public int PageNumber { get; set; } = Configuration.DefaultPageSize;
    }
}
