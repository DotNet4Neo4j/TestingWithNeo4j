namespace TestingWithNeo4j.NUnit
{
    using System.Linq;
    using FluentAssertions;
    using global::NUnit.Framework;

    public class NunitTests
    {
        [TestFixture]
        public class CommunityDriver
        {
            [Test]
            public void RetrieveFromNeo4j()
            {
                var community = new CommunityNeo4jClient(TestContext.CurrentContext.TestDirectory);
                var result = community.GetBusinessObjects().ToList();
                result.Should().HaveCount(1);
                result.Single().Id.Should().Be(101);
                result.Single().Value.Should().Be("FooBar");
            }
        }

        [TestFixture]
        public class OfficialDriver
        {
            [Test]
            public void RetrieveFromNeo4j()
            {
                var official = new Official(TestContext.CurrentContext.TestDirectory);
                var result = official.GetBusinessObjects().ToList();
                result.Should().HaveCount(1);
                result.Single().Id.Should().Be(101);
                result.Single().Value.Should().Be("FooBar");
            }
        }
    }
}