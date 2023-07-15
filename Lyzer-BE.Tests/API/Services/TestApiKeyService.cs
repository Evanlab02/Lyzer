using Lyzer_BE.API.DTOs;
using Lyzer_BE.API.Services.Concrete;
using Lyzer_BE.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzer_BE.Tests.API.Services
{
    [TestFixture]
    public class TestApiKeyService
    {
        ApiKeyService apiKeyService;

        [SetUp]
        public void Setup()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MONGODB_CONNECTION")))
                Environment.SetEnvironmentVariable("MONGODB_CONNECTION", "mongodb://localhost:27017");

            apiKeyService = new ApiKeyService();

            MongoController<HashedApiKeyDto> mongoController = new("ApiKeys", "SaltAndHash");

            List<HashedApiKeyDto> apiKeys = new()
            {
                new HashedApiKeyDto()
                {
                    UserName = "IAMAdmin",
                    HashedApiSalt = "fh7WTlzkmeprBbJkHwWuFStJLvrg5Spv+CyTiQKPkQNLC+bcAwK2jSUyIRj6Vd3RfsBzfFRafdE5pmysPtXsPevheAeoCgnmoVHCEVG6Nbm1sWjz/DGOyRofgTStrxL1H7n59e0QZKWao080Ecz/m1bsuh/2m2pVtvun9q4WiWQ=",
                    HashedApiToken = "sDF6DGHTgXix7WGsqAdv8ljJwjvZXh0mnwaSTGCR6y0cSP0aE0Z6jFfjLs/rLg44NP8/sIFAOfbOPf6+NZzEtQ=="
                }
            };

            mongoController.InsertManyIntoCollection(apiKeys);
        }

        [TearDown]
        public void Teardown()
        {
            MongoController<HashedApiKeyDto> mongoController = new MongoController<HashedApiKeyDto>("ApiKeys", "SaltAndHash");
            mongoController.DestroyCollection();
        }

        [Test]
        public void GenerateToken_ShouldReturnToken()
        {
            ApiKeyDto apiKey = apiKeyService.GenerateKey("username");
            Assert.Multiple(() =>
            {
                Assert.That(apiKey, Is.Not.Null);
                Assert.That(apiKey.ApiToken, Is.Not.Null);
                Assert.That(apiKey.HashedApiToken, Is.Not.Null);
                Assert.That(apiKey.HashedApiSalt, Is.Not.Null);
                Assert.That(apiKey.UserName, Is.EqualTo("username"));
            });
        }

        [Test]
        public void VerifyToken_ShouldReturnTrue()
        {
            ApiKeyUserDto apiKey = new ApiKeyUserDto()
            {
                UserName = "IAMAdmin",
                ApiToken = "456e9d5f-72b3-4e9d-a0c2-ace88b8ef32b"
            };
            AuthResultDto auth = apiKeyService.VerifyToken(apiKey).Result;
            Assert.Multiple(() =>
            {
                Assert.That(auth, Is.Not.Null);
                Assert.That(auth.ValidToken, Is.True);
            });
        }

        [Test]
        public void VerifyTokenWithInvalidUserName_ShouldReturnFalse()
        {
            ApiKeyUserDto apiKey = new ApiKeyUserDto()
            {
                UserName = "IAM",
                ApiToken = "456e9d5f-72b3-4e9d-a0c2-ace88b8ef32b"
            };
            AuthResultDto auth = apiKeyService.VerifyToken(apiKey).Result;
            Assert.Multiple(() =>
            {
                Assert.That(auth, Is.Not.Null);
                Assert.That(auth.ValidToken, Is.False);
            });
        }

        [Test]
        public void VerifyTokenWithInvalidApiToken_ShouldReturnFalse()
        {
            ApiKeyUserDto apiKey = new ApiKeyUserDto()
            {
                UserName = "IAMAdmin",
                ApiToken = null
            };
            AuthResultDto auth = apiKeyService.VerifyToken(apiKey).Result;
            Assert.Multiple(() =>
            {
                Assert.That(auth, Is.Not.Null);
                Assert.That(auth.ValidToken, Is.False);
            });
        }
    }
}
