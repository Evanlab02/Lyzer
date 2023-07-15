using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Lyzer_BE.API.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ApiKeyDTO
    {
        public string? UserName { get; set; }
        public string? ApiToken { get; set; }
        public byte[]? HashedApiToken { get; set; }
        public byte[]? HashedApiSalt { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ApiKeyUserDTO
    {
        public string? UserName { get; set; }
        public string? ApiToken { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [BsonIgnoreExtraElements]
    public class HashedApiKeyDTO
    {
        public string? UserName { get; set; }
        public string? HashedApiToken { get; set; }
        public string? HashedApiSalt { get; set; }
    }

    public class AuthResultDTO
    {
        public bool ValidToken { get; set; }
    }
}
