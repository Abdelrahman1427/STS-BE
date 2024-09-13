using STS.DataTransferObjects.Helpers;
using STS.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace STS.Infrastructure.Extentions
{
    internal static class PaginateExtensions
    {
        internal static IPagination<T> ToPaginate<T>(this IEnumerable<T> source, int index, int size, int from = 0)
        {
            return new Pagination<T>(source, index, size, from);
        }
        internal static async Task<Pagination<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size,
            int from = 0, CancellationToken cancellationToken = default)
        {
            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((index - from) * size)
                .Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

            var list = new Pagination<T>
            {
                Index = index,
                Size = size,
                From = from,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };

            return list;
        }
    }
}