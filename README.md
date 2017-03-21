# Testing .NET code against Neo4j 

This is an example solution showing a super simple set of tests against a Neo4j server that is started at the beginning of the test, and shut down afterwards.
In each of the projects, the server is:

1. Installed as a service
2. Started
3. Populated with data
4. Tested against
5. Stopped
6. Uninstalled

## Bits to check before you run these tests

*IF* you have a server already installed on your machine - this will **wipe** it, so be cautious!