namespace TestingWithNeo4j.TestUtilities
{
    public interface IOutputWriter
    {
        void WriteLine(object obj);
        void Write(object obj);
        void WriteLine(string msg);
        void Write(string msg);
    }

    public abstract class BaseOutputWriter : IOutputWriter
    {
        public virtual void WriteLine(object obj)
        {
            WriteLine(obj?.ToString() ?? string.Empty);
        }

        public virtual void Write(object obj)
        {
            Write(obj?.ToString() ?? string.Empty);
        }

        public abstract void WriteLine(string msg);
        public abstract void Write(string msg);
    }
}