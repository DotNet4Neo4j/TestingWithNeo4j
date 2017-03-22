namespace TestingWithNeo4j
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public static class Config
    {
        public static Settings GetSettings(string basePath)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath ?? Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            return Settings.FromConfig(config);
        }
    }
}