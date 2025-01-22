using LanguageExt;

namespace Safa.Domain.Types;

public record Fraction
{
    public decimal Value { get; }

    public static Fraction operator +(Fraction a, Fraction b) => new(a.Value + b.Value);
    public static Fraction operator *(Fraction a, Fraction b) => new(a.Value * b.Value);
    public static Fraction operator -(Fraction a, Fraction b) => new(a.Value - b.Value);

    public static readonly Func<decimal, Option<Fraction>> TryCreate = value => new Fraction(value);
    
    public static Fraction Zero => new(0m);

    private Fraction(decimal value)
    {
        Value = value;
    }
}