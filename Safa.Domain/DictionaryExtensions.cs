using LanguageExt;

using static LanguageExt.Prelude;

namespace Safa.Domain;

public static class DictionaryExtensions
{
    public static Option<TValue> Find<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
        dictionary.TryGetValue(key, out var value)
            ? Some(value)
            : None;
}