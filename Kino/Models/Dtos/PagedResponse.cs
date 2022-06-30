namespace Kino.Models.Dtos;

public class PagedResponse<T>
{
    public PagedResponse(T source, int totalCount, RequestParameters requestParameters)
    {
        Content = source;
        TotalPages = (int)Math.Ceiling(totalCount / (double)requestParameters.Size);
        TotalCount = totalCount;
    }

    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public T Content { get; set; }
}