using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Extensions;

public static class IQueryablePaginateExtensions
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(
        this IQueryable<T> source,
        int index,
        int size,
        CancellationToken cancellationToken = default
    )
    {
        List<T> items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken);

        int count = items.Count > size ? items.Count + index * size : items.Count;

        return new Paginate<T>
        {
            Index = index,
            Count = count,
            Items = items.Take(size).ToList(),
            Size = size,
            Pages = (int)Math.Ceiling(count / (double)size)
        };
    }

    public static Paginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size)
    {
        int count = source.Count();
        var items = source.Skip(index * size).Take(size).ToList();

        Paginate<T> list =
            new()
            {
                Index = index,
                Size = size,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };
        return list;
    }

}