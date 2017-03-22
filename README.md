# Testing .NET code against Neo4j 

This is an example solution showing a super simple set of tests against a Neo4j server that is started at the beginning of the test, and shut down afterwards.
In each of the projects, the server is:

1. Installed as a service
2. Started
3. Populated with data
4. Tested against
5. Stopped
6. Uninstalled

*IF* you have a server already installed on your machine - and you run this with out changing any settings - this will **wipe** it, so be **cautious**!

## Bits to check before you run these tests

1. You need to install the Windows SDK (for PowerShell automation)
2. You will need to run the tests as an *Administrator*
   - This includes either Visual Studio (if using the built in test runner / ReSharper) or a PowerShell/Command Prompt 
   - This is because we're installing a service to do the tests.

## Step by Step

1. Download the **ZIP** version of Neo4j from the [Neo4j Website](https://neo4j.com/download/other-releases/) and unzip into a folder of your choice.
2. [Optional] Edit the `conf/neo4j.conf`
   - Change `dbms.windows_service_name=` to something meaningful, for example: `neo4j-integration-testserver`
   - Change the ports for Bolt/REST endpoints
3. Run Visual Studio as an Administrator
4. Edit the `app.config` file to point to the server - in particular if you changed any port / security settings in step 2
   - This is shared between all the projects, so you only need to edit one of them.
5. Edit the `appsettings.json` file to point to the server as well.
   - This is shared between all the projects, so you only need to edit one of them.
6. Run the tests!
   - Should work with 
     - VS Test Runner (group by Project to get the best output)
     - ReSharper
     - XUnit Console Runner (XUnit only Obvs)
     - NUnit Console Runner (NUnit only again - Obvs)

## Common Issues

- Not Running as Admin
  - Because we need to install a new service, you need to be an Administrator
- Unable to find server to run tests
  - Check your config (`appsettings.json` and `app.config`) are in sync and both point to the right place