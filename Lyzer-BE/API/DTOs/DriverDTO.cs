using System.Diagnostics.CodeAnalysis;

namespace Lyzer_BE.API.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DriverDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Points { get; set; }
        public string Constructer { get; set; }
    }
}
