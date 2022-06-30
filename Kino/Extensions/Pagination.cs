using Kino.Models.Dtos;

namespace Kino.Extensions;

public static class Pagination
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, RequestParameters parameters) =>
        source.Skip((parameters.Page - 1) * parameters.Size)
            .Take(parameters.Size);
}