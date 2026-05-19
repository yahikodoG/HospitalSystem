using Application.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize)
    {
        var totalCount = await query.CountAsync();

        var currentPage = page > 0 ? page : 1;
        var currentPageSize = pageSize > 0 ? pageSize : 10;


        var items = await query
            .Skip((currentPage - 1) * currentPageSize)
            .Take(currentPageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalItems = totalCount,
            Page = currentPage,
            PageSize = currentPageSize
        };
    }
}