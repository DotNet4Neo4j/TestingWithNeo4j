namespace TestingWithNeo4j.Xunit
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using FluentAssertions;
    using global::Xunit;
    using global::Xunit.Abstractions;
    using global::Xunit.Sdk;

    public class XUnitTests
    {
        [Collection("Neo4j-Server Collection")]
        public class CommunityDriver : TestWithFixture
        {
            public CommunityDriver(ITestOutputHelper output, Neo4jFixture fixture)
                : base(output, fixture)
            {
            }

            [Fact]
            public void RetrieveFromNeo4j()
            {
                var community = new CommunityNeo4jClient(GetExecutingLocation());
                var result = community.GetBusinessObjects().ToList();

                result.Should().HaveCount(1);
                result.Single().Id.Should().Be(101);
                result.Single().Value.Should().Be("FooBar");
            }
        }

        [Collection("Neo4j-Server Collection")]
        public class OfficialDriver : TestWithFixture
        {
            public OfficialDriver(ITestOutputHelper output, Neo4jFixture fixture)
                : base(output, fixture)
            {
            }

            [Fact]
            public void RetrieveFromNeo4j()
            {
                var official = new Official(GetExecutingLocation());
                var result = official.GetBusinessObjects().ToList();
                result.Should().HaveCount(1);
                result.Single().Id.Should().Be(101);
                result.Single().Value.Should().Be("FooBar");
            }
        }
    }
}