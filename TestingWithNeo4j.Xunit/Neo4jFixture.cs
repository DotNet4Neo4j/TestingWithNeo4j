namespace TestingWithNeo4j.Xunit
{
    using System;
    using global::Xunit;
    using TestingWithNeo4j.TestUtilities;

    public class Neo4jFixture : IDisposable
    {
        private readonly PowerShellRunner _powerShellRunner;

        public Neo4jFixture()
        {
            _powerShellRunner = new PowerShellRunner(new XUnitOutputWrapper());
            _powerShellRunner.InstallAndStartNeo4j();
            _powerShellRunner.InitializeDb();
        }

        public void Dispose()
        {
            _powerShellRunner.StopAndUninstallNeo4j();
        }
    }

    [CollectionDefinition("Neo4j-Server Collection")]
    public class Neo4jServerCollection : ICollectionFixture<Neo4jFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}