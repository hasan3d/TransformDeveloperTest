using TransformDeveloperTest.Domain.ValueObjects;

namespace TransformDeveloperTest.Application.Tfl.Extensions;
public static class LineColourMapExtensions
{
    private static readonly Dictionary<string, Colour> _lineColours = new(){
        {"hammersmith-city", Colour.Pink},
        {"metropolitan", Colour.VenetianRed},
        {"jubilee", Colour.Grey},
        {"northern", Colour.Black},
        {"victoria", Colour.LightBlue},
        {"waterloo-city", Colour.Turquoise},
        {"bakerloo", Colour.Brown},
        {"central", Colour.Red},
        {"circle", Colour.Yellow},
        {"district", Colour.Green},
        {"piccadilly", Colour.Blue}
    };

    public static string MapLineColour(this string lineId)
    {
        if (string.IsNullOrEmpty(lineId)) throw new ArgumentNullException(nameof(lineId));

        return _lineColours[lineId];
    }

}
