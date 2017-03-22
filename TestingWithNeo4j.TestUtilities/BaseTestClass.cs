namespace TestingWithNeo4j.TestUtilities
{
    using System;
    using System.IO;
    using System.Reflection;

    public abstract class BaseTestClass
    {
        protected static string GetExecutingLocation()
        {
            return Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        }
    }
}