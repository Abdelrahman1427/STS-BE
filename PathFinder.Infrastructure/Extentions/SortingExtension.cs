using STS.DataTransferObjects.Helpers;
using STS.SharedKernel.Extensions;
using STS.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace STS.Infrastructure.Extentions
{
    internal static class SortingExtensions
    {
        internal static IQueryable<T> ToSort<T>(this IQueryable<T> source, string? Sorting, bool IsAsc)
        {
            if (Sorting != "")
            {
                var propNames = Sorting?.Split(",");
                if (propNames.Count() > 0)
                {
                    source = source.SortBy(propNames[0], IsAsc);
                    for (int item = 1; item < propNames.Count(); item++)
                    {
                        source = source.SortBy(propNames[item], IsAsc, false);
                    }
                }
                else
                    source = source.SortBy(Sorting, IsAsc);
            }
            return source;
        }
    }
}