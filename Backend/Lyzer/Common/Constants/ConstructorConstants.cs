namespace Lyzer.Common.Constants
{
    public class ConstructorConstants
    {
        public static class ConstructorColours
        {
            private static readonly List<ConstructorColourDTO> _constructorColours = new()
            {
                new ConstructorColourDTO { Constructor = "mclaren", Colour = "#FF9800" },
                new ConstructorColourDTO { Constructor = "ferrari", Colour = "#E8002D" },
                new ConstructorColourDTO { Constructor = "red_bull", Colour = "#1E5BC6" },
                new ConstructorColourDTO { Constructor = "mercedes", Colour = "#6CD3BF" },
                new ConstructorColourDTO { Constructor = "aston_martin", Colour = "#2D826D" },
                new ConstructorColourDTO { Constructor = "alpine", Colour = "#2293D1" },
                new ConstructorColourDTO { Constructor = "haas", Colour = "#B6BABD" },
                new ConstructorColourDTO { Constructor = "rb", Colour = "#469BFF" },
                new ConstructorColourDTO { Constructor = "sauber", Colour = "#51B7E3" },
                new ConstructorColourDTO { Constructor = "williams", Colour = "#005AFF" }
            };

            public static IReadOnlyList<ConstructorColourDTO> GetConstructorColours() => _constructorColours;

            //Convert this to helper method once helper methods are created
            public static string GetColourForConstructor(string? constructor)
            {
                if (string.IsNullOrEmpty(constructor))
                    return "None";

                return GetConstructorColours().First(c => c.Constructor == constructor).Colour;
            }
        }

        public class ConstructorColourDTO
        {
            public required string Constructor { get; set; }
            public required string Colour { get; set; }
        }
    }
}
