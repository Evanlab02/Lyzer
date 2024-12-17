namespace Lyzer.Common.DTO
{
    public class CircuitDTO
    {
        public string CircuitId { get; set; }
        public required string Url { get; set; }
        public required string CircuitName { get; set; }
        public required LocationDTO Location { get; set; }
    }
}