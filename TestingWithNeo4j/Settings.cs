namespace TestingWithNeo4j
{
    using Microsoft.Extensions.Configuration;

    public class Settings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string BoltEndPoint { get; set; }
        public string RestEndPoint { get; set; }

        public static Settings FromConfig(IConfigurationRoot config)
        {
            return new Settings
            {
                Username = config["neo4j:username"],
                Password = config["neo4j:password"],
                BoltEndPoint = config["neo4j:boltEndPoint"],
                RestEndPoint = config["neo4j:restEndPoint"]
            };
        }
    }
}