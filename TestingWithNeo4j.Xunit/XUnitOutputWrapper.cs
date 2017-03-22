namespace TestingWithNeo4j.Xunit
{
    using System.Diagnostics;
    using TestingWithNeo4j.TestUtilities;

    public class XUnitOutputWrapper : BaseOutputWriter
    {
       public override void WriteLine(string msg)
        {
            Trace.WriteLine(msg);
        }

        public override void Write(string msg)
        {
            Trace.Write(msg);
        }
    }
}