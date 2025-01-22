using LanguageExt;
using static LanguageExt.Prelude;

namespace Safa.Domain.Types;

public record Amount : IComparable<Amount>
{
    public static Amount operator +(Amount a, Amount b) => new(a.Value + b.Value);
    public static Amount operator *(Amount a, Amount b) => new(a.Value * b.Value);
    public static Amount operator *(Amount a, Fraction b) => new(a.Value * b.Value);
    public static Amount operator -(Amount a, Amount b) => new(a.Value - b.Value);

    public decimal Value { get; }
    
    public static Amount Zero => new(0m);

    public static readonly Func<decimal, Option<Amount>> TryCreate = value =>
        IsValid(value)
            ? Some(new Amount(value))
            : None;

    public int CompareTo(Amount other)
    {
        if (this.Value < other.Value)
        {
            return -1;
        }

        if (this.Value > other.Value)
        {
            return 1;
        }

        return 0;
    }

    private Amount(decimal value)
    {
        Value = value;
    }

    private static bool IsValid(Option<decimal> input) => input >= 0m;
}