using TransformDeveloperTest.Domain.Common;
using TransformDeveloperTest.Domain.Exceptions;

namespace TransformDeveloperTest.Domain.ValueObjects;

public class Colour : ValueObject
{
    static Colour()
    {
    }

    private Colour()
    {
    }

    private Colour(string code)
    {
        Code = code;
    }

    public static Colour From(string code)
    {
        var colour = new Colour { Code = code };

        if (!SupportedColours.Contains(colour))
        {
            throw new UnsupportedColourException(code);
        }

        return colour;
    }

    public static Colour Red => new("#FF5733");

    public static Colour VenetianRed => new ("#9B0056");

    public static Colour Yellow => new("#FFFF66");

    public static Colour Green => new("#CCFF99");

    public static Colour Blue => new("#6666FF");

    public static Colour LightBlue => new("#ADD8E6");

    public static Colour Black => new("#000000");

    public static Colour Grey => new("#999999");

    public static Colour Pink => new("#FFC0CB");

    public static Colour Turquoise => new("#30D5C8");

    public static Colour Brown => new("#964B00");

    public string Code { get; private set; } = "#000000";

    public static implicit operator string(Colour colour)
    {
        return colour.ToString();
    }

    public static explicit operator Colour(string code)
    {
        return From(code);
    }

    public override string ToString()
    {
        return Code;
    }

    protected static IEnumerable<Colour> SupportedColours
    {
        get
        {
            yield return Brown;
            yield return Red;
            yield return LightBlue;
            yield return Yellow;
            yield return Green;
            yield return Blue;
            yield return Turquoise;
            yield return Grey;
            yield return Black;
            yield return Pink;
            yield return VenetianRed;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
