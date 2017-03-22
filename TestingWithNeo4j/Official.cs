namespace TestingWithNeo4j
{
    using System.Collections.Generic;
    using Neo4j.Driver.V1;

    public class Official : SettingsUser
    {


        public IEnumerable<BusinessObject> GetBusinessObjects()
        {
            IAuthToken authToken = AuthTokens.None;
            if (!string.IsNullOrWhiteSpace(Settings.Username) && !string.IsNullOrWhiteSpace(Settings.Password))
            {
                authToken = AuthTokens.Basic(Settings.Username, Settings.Password);
            }


            using (var driver = GraphDatabase.Driver(Settings.BoltEndPoint, authToken))
            {
                using (var session = driver.Session())
                {
                    var statementResult = session.Run($"MATCH (bo:{BusinessObject.Labels}) RETURN bo");
                    foreach (var record in statementResult)
                        yield return record.ToBusinessObject("bo");
                }
            }
        }

        public Official(string basePath) : base(basePath)
        {
        }
    }
}