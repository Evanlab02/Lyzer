using Lyzer_BE.API.Controllers;
using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Interfaces;
using Moq;

namespace Lyzer_BE.Tests.API.Controllers
{
    [TestFixture]
    internal class TestApiKeyController
    {
        [Test]
        public void GenerateUserName_ShouldReturnApiKey()
        {
            var mockedApiKey = new ApiKeyDto()
            {
                ApiToken = "token",
                UserName = "username",
                HashedApiSalt = null,
                HashedApiToken = null
            };

            var apiKeyServiceMock = new Mock<IApiKeyService>();
            var apiKeyController = new ApiKeyController(apiKeyServiceMock.Object);

            apiKeyServiceMock.Setup(service => service.GenerateKey(It.Is<string>(userName => userName == "username")))
                .Returns(mockedApiKey);

            ApiKeyDto? result = apiKeyController.GenerateApiKey("username");
            Assert.That(result, Is.EqualTo(mockedApiKey));
        }

        [Test]
        public void Authenticate_ShouldAuthResult()
        {
            var mockedAuth = new AuthResultDto()
            {
                ValidToken = true
            };

            var apiKeyServiceMock = new Mock<IApiKeyService>();
            var apiKeyController = new ApiKeyController(apiKeyServiceMock.Object);

            apiKeyServiceMock.Setup(service => service.VerifyToken(
                It.Is<ApiKeyUserDto>(
                    auth => auth.ApiToken == "token" && auth.UserName == "username")
                )
            ).Returns(Task.FromResult(mockedAuth));

            Task<AuthResultDto>? result = apiKeyController.VerifyUser(new ApiKeyUserDto()
            {
                ApiToken = "token",
                UserName = "username"
            });

            Assert.That(result.Result, Is.EqualTo(mockedAuth));
        }
    }
}
