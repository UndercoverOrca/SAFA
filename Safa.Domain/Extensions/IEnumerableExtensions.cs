using Safa.Domain.Types;

namespace Safa.Domain.Extensions;

public static class IEnumerableExtensions
{
    public static Amount Sum(this IEnumerable<Amount> source) =>
        source
            .DefaultIfEmpty(Amount.Zero)
            .Aggregate((x, y) => x + y);
}