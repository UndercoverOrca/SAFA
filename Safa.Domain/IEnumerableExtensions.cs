namespace Safa.Domain;

public static class IEnumerableExtensions
{
    public static Amount Sum(this IEnumerable<Amount> source) =>
        source
            .DefaultIfEmpty(Amount.Zero)
            .Aggregate((x, y) => x + y);
}