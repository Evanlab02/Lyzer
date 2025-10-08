namespace Lyzer.Common.Constants.Colors
{
    /// <summary>
    /// Defines color constants for F1 constructors/teams.
    /// </summary>
    public static class ConstructorColorConstants
    {
        /// <summary>
        /// Data transfer object representing a constructor and its associated color.
        /// </summary>
        public class ConstructorColorDTO
        {
            /// <summary>
            /// The name of the constructor/team.
            /// </summary>
            public required string Constructor { get; set; }

            /// <summary>
            /// The hex color code associated with the constructor (e.g., "#FF9800").
            /// </summary>
            public required string Colour { get; set; }
        }

        /// <summary>
        /// Internal collection of constructor colors for the current F1 season.
        /// </summary>
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

        /// <summary>
        /// Gets the read-only collection of all constructor colors.
        /// </summary>
        /// <returns>A read-only list of constructor color mappings.</returns>
        public static IReadOnlyList<ConstructorColorDTO> GetConstructorColours() => _constructorColours;

        /// <summary>
        /// Retrieves the color code for a specific constructor.
        /// </summary>
        /// <param name="constructor">The name of the constructor to look up.</param>
        /// <returns>The hex color code for the constructor, or an empty string if not found or if the input is null/empty.</returns>
        public static string GetColorForConstructor(string? constructor)
        {
            if (string.IsNullOrEmpty(constructor))
                return "";

            ConstructorColorDTO? colorDto = GetConstructorColours().FirstOrDefault(c => c.Constructor == constructor);
            return colorDto != null ? colorDto.Colour : "";
        }
    }
}