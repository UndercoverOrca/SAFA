using LanguageExt;

namespace Safa.Infrastructure;

public static class EnumerableExtensions
{
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> source)
    {
        var value = source.FirstOrDefault();
        return value is null
            ? Prelude.None
            : Prelude.Some(value);
    }
}