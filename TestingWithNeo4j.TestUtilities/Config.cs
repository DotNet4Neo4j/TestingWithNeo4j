namespace TestingWithNeo4j.TestUtilities
{
    public static class Config
    {
        public static class Neo4j
        {
            public static string RootLocation => $"{nameof(Neo4j)}.{nameof(RootLocation)}";
            public static string Username => $"{nameof(Neo4j)}.{nameof(Username)}";
            public static string Password => $"{nameof(Neo4j)}.{nameof(Password)}";
            public static string RestEndPoint => $"{nameof(Neo4j)}.{nameof(RestEndPoint)}";
            public static string BoltEndPoint => $"{nameof(Neo4j)}.{nameof(BoltEndPoint)}";
        }
    }
}