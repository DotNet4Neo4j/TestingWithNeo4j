namespace TestingWithNeo4j.TestUtilities
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object obj)
        {
            return obj?.GetType()
                .GetProperties()
                .ToDictionary(property => property.Name, property => property.GetValue(obj));
        }
    }
}