using MongoDB.Driver;

namespace QuestionnaireApp.Models
{
    public class DatabaseConnection
    {
        private readonly IMongoDatabase _database;

        public DatabaseConnection(IConfiguration configuration)
        {
            var sections = configuration.GetSection("ConnectionStrings");
            string databaseName = sections.GetValue<string>("DatabaseName");
            string host = sections.GetValue<string>("MongoConnection");
            // Ensuring the connection string is properly formatted
            string connectionString = $"mongodb://{host}";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
