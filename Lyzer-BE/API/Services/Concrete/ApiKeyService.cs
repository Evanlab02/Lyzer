using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace Lyzer_BE.API.Services.Concrete
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly MongoController<HashedApiKeyDto> _mongoController;

        public ApiKeyService()
        {
            _mongoController = new MongoController<HashedApiKeyDto>("ApiKeys", "SaltAndHash");
        }

        public ApiKeyDto GenerateKey(string userName)
        {
            Guid guid = Guid.NewGuid();
            ApiKeyDto key = new()
            {
                UserName = userName,
                ApiToken = guid.ToString()
            };

            using (var hmac = new HMACSHA512())
            {
                key.HashedApiSalt = hmac.Key;
                key.HashedApiToken = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key.ApiToken));
            }

            return key;
        }

        public async Task<AuthResultDto> VerifyToken(ApiKeyUserDto userApiKey)
        {
            var result = new AuthResultDto()
            {
                ValidToken = false
            };

            HashedApiKeyDto dbApiKey = await _mongoController.FindOneFromCollection(Builders<HashedApiKeyDto>.Filter.Eq(apiKey => apiKey.UserName, userApiKey.UserName));
            if (userApiKey.ApiToken == null || dbApiKey == null || dbApiKey.HashedApiSalt == null || dbApiKey.HashedApiToken == null || dbApiKey.UserName != userApiKey.UserName)
            {
                return result;
            }

            using (var hmac = new HMACSHA512(Convert.FromBase64String(dbApiKey.HashedApiSalt)))
            {
                var userHashedToken = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userApiKey.ApiToken));
                if (userHashedToken.SequenceEqual(Convert.FromBase64String(dbApiKey.HashedApiToken)))
                {
                    result.ValidToken = true;
                }
            }

            return result;
        }
    }
}
