namespace Lyzer.Common.DTO
{
    public class DriverDTO
    {
        public required string DriverId { get; set; }
        public string? PermanentNumber { get; set; }
        public string? Code { get; set; }
        public string? Url { get; set; }
        public required string GivenName { get; set; }
        public required string FamilyName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}