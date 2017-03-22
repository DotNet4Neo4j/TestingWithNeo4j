namespace TestingWithNeo4j.MsTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestingWithNeo4j.TestUtilities;

    [TestClass]
    public class Setup
    {
        /* 
         * For this to work - JAVA_HOME needs to point to c:\program files\Java *not* c:\program files (x86)\Java
         * MS Test seems to look in the wrong place - something to do with x86 vs x64
         */

        private static readonly PowerShellRunner PowerShellRunner = new PowerShellRunner(new MsTestOutputWrapper());

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            PowerShellRunner.InstallAndStartNeo4j();
            PowerShellRunner.InitializeDb();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            PowerShellRunner.StopAndUninstallNeo4j();
        } 
    }
}