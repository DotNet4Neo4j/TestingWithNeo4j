namespace TestingWithNeo4j
{
    using Neo4j.Driver.V1;

    internal static class RecordExtensions
    {
        public static BusinessObject ToBusinessObject(this IRecord record, string identifier)
        {
            var node = record[identifier].As<INode>();

            var bo = new BusinessObject
            {
                Id = node["Id"].As<int>(),
                Value = node["Value"].As<string>()
            };

            return bo;
        }
    }
}