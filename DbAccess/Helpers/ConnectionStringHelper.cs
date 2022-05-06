
namespace DbAccess.Helpers
{
    using System;
    using Interfaces;

    internal sealed class ConnectionStringHelper : IConnectionStringHelper
    {
        private static ConnectionStringHelper? instance = null;

        public static ConnectionStringHelper Instance => instance ??= new ConnectionStringHelper();
        public string ConnectionString { get; set; } = "";

        private ConnectionStringHelper() => ReadConnectionString();

        private void ReadConnectionString()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            const string file = @"ConnectionString\sikrit-key.txt";
            var combined = Path.Combine(documents, file);
            if (File.Exists(combined)) ConnectionString = File.ReadAllText(combined);
        }
    }
}
