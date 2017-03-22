namespace TestingWithNeo4j.MsTest
{
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestingWithNeo4j.TestUtilities;

    public class MsTests
    {
        [TestClass]
        public class CommunityDriver : BaseTestClass
        {
            [TestMethod]
            public void RetrieveFromNeo4j()
            {
                var community = new CommunityNeo4jClient(GetExecutingLocation());
                var result = community.GetBusinessObjects().ToList();
                result.Should().HaveCount(1);
                result.Single().Id.Should().Be(101);
                result.Single().Value.Should().Be("FooBar");
            }
        }

        [TestClass]
        public class OfficialDriver : BaseTestClass
        {
            [TestMethod]
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