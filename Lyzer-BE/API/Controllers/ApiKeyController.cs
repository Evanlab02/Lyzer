﻿using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public ApiKeyDTO GenerateApiKey(string username)
        {
            return _apiKeyService.GenerateKey(username);
        }

        [HttpPost("authenticate")]
        public async Task<AuthResultDTO> GenerateApiKey(ApiKeyUserDTO userApiKey)
        {
            return await _apiKeyService.VerifyToken(userApiKey);
        }
    }
}