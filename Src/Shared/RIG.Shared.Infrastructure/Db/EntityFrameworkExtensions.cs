using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RIG.Shared.Infrastructure.Db
{
    public static class EntityFrameworkExtensions
    {
        public static async Task<(int, List<T>)> PaginatedQueryAsync<T>(this IQueryable<T> queryable, int offset, int limit, CancellationToken cancellationToken)
        {
            var paginatedResult = await queryable
                                       .Select(model => new {Model = model, TotalCount = queryable.Count()})
                                       .Skip(offset)
                                       .Take(limit)
                                       .ToListAsync(cancellationToken);

            return (paginatedResult.FirstOrDefault()?.TotalCount ?? 0,
                    paginatedResult.Select(m => m.Model).ToList());
        }

        public static Task<(int, List<T>)> PaginatedQueryAsync<T>(this ICollection<T> queryable, int offset, int limit, CancellationToken cancellationToken)
        {
            var paginatedResult = queryable
                                 .Select(model => new {Model = model, TotalCount = queryable.Count()})
                                 .Skip(offset)
                                 .Take(limit)
                                 .ToList();

            return Task.FromResult((paginatedResult.FirstOrDefault()?.TotalCount ?? 0,
                                    paginatedResult.Select(m => m.Model).ToList()));
        }
    }
}