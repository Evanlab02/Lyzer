using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Lyzer_BE.Utils;
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
            string token = Request.Headers["Lyzer-Api-Token"];
            ApiKeyUserDto userKey = AuthUtils.GenerateApiKey(token);
            return await _apiKeyService.VerifyToken(userKey);
        }
    }
}
