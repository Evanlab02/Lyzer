namespace Lyzer.Common.Constants.Colors
{
    public static class ConstructorColorConstants
    {
        public class ConstructorColorDTO
        {
            public required string Constructor { get; set; }
            public required string Colour { get; set; }
        }

        private static readonly List<ConstructorColorDTO> _constructorColours = new()
        {
            new ConstructorColorDTO { Constructor = "McLaren", Colour = "#FF9800" },
            new ConstructorColorDTO { Constructor = "Ferrari", Colour = "#E8002D" },
            new ConstructorColorDTO { Constructor = "Red Bull", Colour = "#1E5BC6" },
            new ConstructorColorDTO { Constructor = "Mercedes", Colour = "#6CD3BF" },
            new ConstructorColorDTO { Constructor = "Aston Martin", Colour = "#2D826D" },
            new ConstructorColorDTO { Constructor = "Alpine F1 Team", Colour = "#0090FF" },
            new ConstructorColorDTO { Constructor = "Haas F1 Team", Colour = "#B6BABD" },
            new ConstructorColorDTO { Constructor = "RB F1 Team", Colour = "#6692FF" },
            new ConstructorColorDTO { Constructor = "Sauber", Colour = "#00FF00" },
            new ConstructorColorDTO { Constructor = "Williams", Colour = "#005AFF" }
        };

        public static IReadOnlyList<ConstructorColorDTO> GetConstructorColours() => _constructorColours;

        public static string GetColorForConstructor(string? constructor)
        {
            if (string.IsNullOrEmpty(constructor))
                return "";

            ConstructorColorDTO? colorDto = GetConstructorColours().FirstOrDefault(c => c.Constructor == constructor);
            return colorDto != null ? colorDto.Colour : "";
        }
    }
}