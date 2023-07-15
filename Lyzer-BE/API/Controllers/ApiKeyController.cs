using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lyzer_BE.API.Controllers
{
    [Route("api/key")]
    [ApiController]
    public class ApiKeyController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        [HttpGet("generate/{username}")]
        public ApiKeyDto GenerateApiKey(string username)
        {
            return _apiKeyService.GenerateKey(username);
        }

        [HttpGet("authenticate")]
        public async Task<AuthResultDto> VerifyUser()
        {
            try
            {
                string token = Request.Headers["Lyzer-Api-Token"];
                if (token == null || token.Split(" ").Length != 2)
                {
                    return new AuthResultDto()
                    {
                        ValidToken = false
                    };
                }

                var userName = token.Split(" ")[0];
                var apiToken = token.Split(" ")[1];

                ApiKeyUserDto userApiKey = new()
                {
                    UserName = userName,
                    ApiToken = apiToken
                };

                return await _apiKeyService.VerifyToken(userApiKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Console.WriteLine(ex.Message);
                return new AuthResultDto()
                {
                    ValidToken = false
                };
            }
        }
    }
}
