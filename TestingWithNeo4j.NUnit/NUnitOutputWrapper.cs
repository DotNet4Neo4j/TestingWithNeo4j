namespace TestingWithNeo4j.NUnit
{
    using global::NUnit.Framework;
    using TestingWithNeo4j.TestUtilities;

    public class NUnitOutputWrapper : BaseOutputWriter
    {
        public override void WriteLine(string msg)
        {
            TestContext.Progress.WriteLine(msg);
        }

        public override void Write(string msg)
        {
            TestContext.Progress.Write(msg);
        }
    }
}