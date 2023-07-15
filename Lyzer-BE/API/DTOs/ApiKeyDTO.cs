using MongoDB.Bson.Serialization.Attributes;

namespace Lyzer_BE.API.DTOs
{
    public class ApiKeyDTO
    {
        public string UserName { get; set; }
        public string ApiToken { get; set; }
        public byte[] HashedApiToken { get; set; }
        public byte[] HashedApiSalt { get; set; }
    }

    public class ApiKeyUserDTO
    {
        public string UserName { get; set; }
        public string ApiToken { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class HashedApiKeyDTO
    {
        public string UserName { get; set; }
        public string HashedApiToken { get; set; }
        public string HashedApiSalt { get; set; }
    }

    public class AuthResultDTO
    {
        public bool ValidToken { get; set; }
    }
}
