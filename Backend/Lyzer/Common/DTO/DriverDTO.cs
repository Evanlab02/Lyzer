namespace Lyzer.Common.DTO
{
    /// <summary>
    /// Represents an F1 driver with their personal information and identifiers.
    /// </summary>
    public class DriverDTO
    {
        /// <summary>
        /// Unique identifier for the driver.
        /// </summary>
        public required string DriverId { get; set; }

        /// <summary>
        /// The driver's permanent racing number (e.g., "44" for Lewis Hamilton).
        /// </summary>
        public string? PermanentNumber { get; set; }

        /// <summary>
        /// Three-letter driver code (e.g., "HAM", "VER").
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// URL to the driver's Wikipedia page.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// The driver's first/given name.
        /// </summary>
        public required string GivenName { get; set; }

        /// <summary>
        /// The driver's last/family name.
        /// </summary>
        public required string FamilyName { get; set; }

        /// <summary>
        /// The driver's date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// The driver's nationality.
        /// </summary>
        public string? Nationality { get; set; }

        /// <summary>
        /// Gets the driver's full name by combining given name and family name.
        /// </summary>
        /// <returns>The driver's full name in the format "GivenName FamilyName".</returns>
        public string GetFullName()
        {
            return $"{GivenName} {FamilyName}";
        }
    }
}