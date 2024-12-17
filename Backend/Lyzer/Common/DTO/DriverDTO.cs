namespace Lyzer.Common.DTO
{
    public class DriverDTO
    {
        public required string DriverId { get; set; }
        public required string PermanentNumber { get; set; }
        public required string Code { get; set; }
        public required string Url { get; set; }
        public required string GivenName { get; set; }
        public required string FamilyName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Nationality { get; set; }
    }
}