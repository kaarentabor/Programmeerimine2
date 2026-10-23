using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Application.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public static class PagingExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            page = Math.Max(page, 1);
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.CountAsync(cancellationToken)
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            // Väldi suure lehenumbri korral int ületäitumist.
            var skip = ((long)page - 1) * pageSize;
            if (skip >= result.RowCount)
            {
                return result;
            }
            result.Results = await query
                .Skip((int)skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
