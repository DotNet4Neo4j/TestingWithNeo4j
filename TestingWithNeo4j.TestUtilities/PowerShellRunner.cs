namespace TestingWithNeo4j.TestUtilities
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Management.Automation;
    using System.Text;
    using System.Threading;
    using Neo4j.Driver.V1;

    public class PowerShellRunner
    {
        /// <summary>
        ///     Powershell to allow the import of the Management Module.
        /// </summary>
        private const string ElevateExecutionPolicy = "Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass; Get-ExecutionPolicy;";

        /// <summary>
        ///     Sets up the settings for running a Neo4j server for integration tests.
        /// </summary>
        /// <param name="outputWriter">The writer to use to output messages.</param>
        /// <param name="neo4jRoot">The root location of your Neo4j install/extracted zip.</param>
        /// <param name="pauseTime">
        ///     The amount of time to wait for the server to be ready - if using something like GraphAware -
        ///     you probably want to increase this from the default - which is <c>5</c>.
        /// </param>
        public PowerShellRunner(IOutputWriter outputWriter, string neo4jRoot = null, int pauseTime = 5)
        {
            Output = outputWriter;
            PauseTime = pauseTime;

            if (string.IsNullOrWhiteSpace(neo4jRoot))
                neo4jRoot = ConfigurationManager.AppSettings[Config.Neo4j.RootLocation];

            ImportModuleScript = $"Import-Module {neo4jRoot}\\bin\\Neo4j-Management.psd1";
        }

        private string ImportModuleScript { get; }
        private int PauseTime { get; }
        private IOutputWriter Output { get; }

        /// <summary>
        ///     Attempts to install and start a new instance of Neo4j.
        /// </summary>
        /// <exception cref="InvalidOperationException">Raised if any error occurs during setup.</exception>
        public void InstallAndStartNeo4j()
        {
            using (var ps = PowerShell.Create())
            {
                ps.AddScript(ElevateExecutionPolicy);
                var result = ps.Invoke();
                if (result[0].ToString() != "Bypass")
                    throw new InvalidOperationException($"Unable to set the ExecutionPolicy - currently it's set to {result[0]} and it needs to be 'Bypass'.");

                ps.AddScript(ImportModuleScript);
                ps.AddScript("Invoke-Neo4j install-service");
                Output.WriteLine($"Installing Neo4j... Module from: '{ImportModuleScript}'.");
                result = ps.Invoke();
                if (result[0].ToString() != "0")
                    throw new InvalidOperationException($"Unable to INSTALL the Neo4j instance - recieved {result[0]} instead of the expected '0'.");

                DisplayErrors(ps);

                Output.WriteLine("Starting and getting the status of Neo4j instance");
                ps.AddScript("Invoke-Neo4j start; Invoke-Neo4j status");
                result = ps.Invoke();
                if (result[0].ToString() != "0")
                    throw new InvalidOperationException($"Unable to START the Neo4j instance - recieved {result[0]} instead of the expected '0'.");

                if (result[1].ToString() != "0")
                    throw new InvalidOperationException($"Neo4j instance STATUS is not 'running' - recieved {result[1]} instead of the expected '0'.");

                DisplayErrors(ps);

                Output.Write("Pausing for server to have endpoints all ready...");
                for (var i = PauseTime; i >= 0; i--)
                {
                    Thread.Sleep(1000);
                    if (i != 0)
                        Output.Write($"..{i}..");
                }
                Output.WriteLine("..DONE..");
            }
        }

        /// <summary>
        ///     Attempst to stop and uninstall the Neo4j instance.
        /// </summary>
        /// <param name="clearAsWell">If <c>true</c> the DB will be cleared before being uninstalled. Default is <c>false</c>.</param>
        /// <exception cref="InvalidOperationException">Raised if any error occurs during the attempt to uninstall.</exception>
        public void StopAndUninstallNeo4j(bool clearAsWell = false)
        {
            if (clearAsWell)
                ClearDb();

            using (var ps = PowerShell.Create())
            {
                ps.AddScript(ElevateExecutionPolicy);
                ps.AddScript(ImportModuleScript);
                ps.AddScript("Invoke-Neo4j uninstall-service");

                Output.Write("Uninstalling Neo4j instance..");
                var result = ps.Invoke();
                if (result[0].ToString() != "0")
                    throw new InvalidOperationException($"UNINSTALLING the Neo4j instance returned an invalid value, got: {result[0]}, expected '0'.");

                DisplayErrors(ps);
                Output.WriteLine("..COMPLETE");
            }
        }

        /// <summary>
        ///     Gets a <see cref="IDriver" /> instance using the <see cref="ConfigurationManager" /> to get the correct values.
        /// </summary>
        /// <returns></returns>
        private static IDriver GetDriver()
        {
            var username = ConfigurationManager.AppSettings[Config.Neo4j.Username];
            var password = ConfigurationManager.AppSettings[Config.Neo4j.Password];
            var boltEndPoint = ConfigurationManager.AppSettings[Config.Neo4j.BoltEndPoint];

            var authToken = AuthTokens.None;
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                authToken = AuthTokens.Basic(username, password);

            return GraphDatabase.Driver(boltEndPoint, authToken);
        }

        /// <summary>
        ///     Wipes the database clear.
        /// </summary>
        public void ClearDb()
        {
            using (var driver = GetDriver())
            {
                using (var session = driver.Session())
                {
                    session.Run("MATCH (n) DETACH DELETE n").Consume();
                }
            }
        }

        /// <summary>
        ///     Initializes the DB with data.
        /// </summary>
        /// <remarks>
        ///     For a proper session - you'd probably want to <c>LOAD CSV</c> to have a better data source.
        /// </remarks>
        public void InitializeDb()
        {
            using (var driver = GetDriver())
            {
                using (var session = driver.Session())
                {
                    ClearDb();
                    var boParams = new BusinessObject {Id = 101, Value = "FooBar"};
                    session.Run($"CREATE (bo:{BusinessObject.Labels} {{boParams}})", new Dictionary<string, object> {{"boParams", boParams.ToDictionary()}}).Consume();
                }
            }
        }

        /// <summary>
        ///     Checks the <see cref="PowerShell" /> instance for any errors, and writes them to output if there are any. Also
        ///     raises an <see cref="InvalidOperationException" /> if there are any errors.
        /// </summary>
        /// <param name="ps">The <see cref="PowerShell" /> instance to check.</param>
        /// <exception cref="InvalidOperationException">Raised if there are any errors.</exception>
        private void DisplayErrors(PowerShell ps)
        {
            if (ps.Streams.Error.Count == 0)
                return;

            var errorMessage = new StringBuilder("Setup of Neo4j had errors:");
            ps.Streams.Error.ToList().ForEach(x => errorMessage.AppendLine($"\t{x}"));
            Output.WriteLine(errorMessage);

            throw new InvalidOperationException($"Unable to continue tests{Environment.NewLine}{errorMessage}");
        }
    }
}