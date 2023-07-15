using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IApiKeyService
    {
        public ApiKeyDTO GenerateKey(string userName);

        public Task<AuthResultDTO> VerifyToken(ApiKeyUserDTO userApiKey);
    }
}
