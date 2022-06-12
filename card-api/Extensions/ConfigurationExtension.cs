using Microsoft.Extensions.Configuration;

namespace card_api.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetCardStorageName(this IConfiguration config) => config.GetSection("JsonStorage").GetSection("FileName").Value;
    }
}
