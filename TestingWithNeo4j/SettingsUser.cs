namespace TestingWithNeo4j
{
    public abstract class SettingsUser
    {
        protected Settings Settings { get; }

        protected SettingsUser(string basePath)
        {
            Settings = Config.GetSettings(basePath);
        }
    }
}