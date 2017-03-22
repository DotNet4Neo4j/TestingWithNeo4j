namespace TestingWithNeo4j.NUnit
{
    using global::NUnit.Framework;
    using TestingWithNeo4j.TestUtilities;

    [SetUpFixture]
    public class Setup
    {
        private readonly PowerShellRunner _powerShellRunner 
            = new PowerShellRunner(new NUnitOutputWrapper());

        [OneTimeSetUp]
        public void StartNeo4j()
        {
            _powerShellRunner.InstallAndStartNeo4j();
            _powerShellRunner.InitializeDb();
        }

        [OneTimeTearDown]
        public void StopNeo4j()
        {
            _powerShellRunner.StopAndUninstallNeo4j();
        }
    }
}