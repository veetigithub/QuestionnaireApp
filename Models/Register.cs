using MongoDB.Driver;

namespace QuestionnaireApp.Models
{
    public class Register
    {
        private readonly DatabaseConnection _connection;

        public Register(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public T RegisterUser<T>(T record) where T : class
        {
            try
            {
                var collection = _connection.GetCollection<T>(typeof(T).Name);
                var usernameProperty = typeof(T).GetProperty("Username");

                if (usernameProperty != null)
                {
                    var usernameValue = usernameProperty.GetValue(record);

                    var filter = Builders<T>.Filter.Eq("Username", usernameValue);

                    var existingRecord = collection.Find(filter).FirstOrDefault();
                    if (existingRecord != null)
                    {
                        // Username already exists, handle accordingly (e.g., return null or throw an exception)
                        return null;
                    }
                    else
                    {
                        collection.InsertOne(record);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Nyt kämähti :(  " + err.Message);
            }
            return record;
        }
    }
}
