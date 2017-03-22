namespace TestingWithNeo4j
{
    using System;
    using System.Collections.Generic;
    using Neo4jClient;

    // ReSharper disable once InconsistentNaming
    public class CommunityNeo4jClient : SettingsUser
    {
        public IEnumerable<BusinessObject> GetBusinessObjects()
        {
            var gc = new GraphClient(new Uri(Settings.RestEndPoint), Settings.Username, Settings.Password);
            gc.Connect();

            var query = gc.Cypher
                .Match($"(bo:{BusinessObject.Labels})")
                .Return(bo => bo.As<BusinessObject>());

            return query.Results;
        }

        public CommunityNeo4jClient(string basePath) : base(basePath)
        {
        }
    }
}