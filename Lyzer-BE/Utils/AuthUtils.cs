using Lyzer_BE.API.DTOs;

namespace Lyzer_BE.Utils
{
    public class AuthUtils
    {
        public static ApiKeyUserDto GenerateApiKey(string? apiKeyHeader)
        {
            try
            {
                
                if (apiKeyHeader == null || apiKeyHeader.Split(" ").Length != 2)
                {
                    return new ApiKeyUserDto();
                }

                var userName = apiKeyHeader.Split(" ")[0];
                var apiToken = apiKeyHeader.Split(" ")[1];

                ApiKeyUserDto userApiKey = new()
                {
                    UserName = userName,
                    ApiToken = apiToken
                };

                return userApiKey;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ApiKeyUserDto();
            }
        }
    }
}
