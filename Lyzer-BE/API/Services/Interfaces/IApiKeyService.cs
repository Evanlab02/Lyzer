using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.API.Services.Interfaces
{
    public interface IApiKeyService
    {
        public ApiKeyDto GenerateKey(string userName);

        public Task<AuthResultDto> VerifyToken(ApiKeyUserDto userApiKey);
    }
}
