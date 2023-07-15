using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Database;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace Lyzer_BE.API.Services.Concrete
{
    public class ApiKeyService : IApiKeyService
    {
        MongoController<HashedApiKeyDTO> _mongoController;

        public ApiKeyService()
        {
            _mongoController = new MongoController<HashedApiKeyDTO>("ApiKeys", "SaltAndHash");
        }

        public ApiKeyDTO GenerateKey(string userName)
        {
            Guid guid = Guid.NewGuid();
            ApiKeyDTO key = new()
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

        public async Task<AuthResultDTO> VerifyToken(ApiKeyUserDTO userApiKey)
        {
            var result = new AuthResultDTO()
            {
                ValidToken = false
            };

            HashedApiKeyDTO dbApiKey = await _mongoController.FindOneFromCollection(Builders<HashedApiKeyDTO>.Filter.Empty);
            if (dbApiKey.UserName != userApiKey.UserName)
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
