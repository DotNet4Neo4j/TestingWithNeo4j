namespace TestingWithNeo4j.Xunit
{
    using global::Xunit.Abstractions;
    using TestingWithNeo4j.TestUtilities;

    public abstract class TestWithFixture : BaseTestClass
    {
        protected Neo4jFixture Fixture { get; }
        protected ITestOutputHelper Output { get; }
        protected TestWithFixture(ITestOutputHelper output, Neo4jFixture fixture)
        {
            Fixture = fixture;
            Output = output;
        }
    }
}