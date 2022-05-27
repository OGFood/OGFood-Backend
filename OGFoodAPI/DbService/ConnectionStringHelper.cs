namespace OGFoodAPI.DbService
{
    public sealed class ConnectionStringHelper
    {
        public string ConnectionString { get; set; } = "";

        public ConnectionStringHelper() => ReadConnectionString();

        public void ReadConnectionString()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            const string file = @"ConnectionStrings\sikrit-string.txt";
            var combined = Path.Combine(documents, file);
            if (File.Exists(combined)) ConnectionString = File.ReadAllText(combined);
            else if (Environment.GetEnvironmentVariable("CNNSTR") != "") ConnectionString = Environment.GetEnvironmentVariable("CNNSTR");
            else if (Environment.GetEnvironmentVariable("CUSTOMCONNSTR_ttmongodb") != "") ConnectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_ttmongodb");
        }
    }
}