namespace TestingWithNeo4j.MsTest
{
    using System;
    using TestingWithNeo4j.TestUtilities;

    internal class MsTestOutputWrapper : BaseOutputWriter
    {
        public override void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }

        public override void Write(string msg)
        {
            Console.Write(msg);
        }
    }
}