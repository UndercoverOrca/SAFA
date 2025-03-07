﻿using LanguageExt;
using static LanguageExt.Prelude;

namespace Safa.Infrastructure;

public static class EnumerableExtensions
{
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> source)
    {
        var value = source.FirstOrDefault();
        return value is null
            ? None
            : Some(value);
    }
}