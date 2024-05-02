using MongoDB.Driver;

namespace QuestionnaireApp.Models
{
    public class SurveyManipulator
    {
        private readonly DatabaseConnection _connection;

        public SurveyManipulator(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public T CreateSurvey<T>(T survey) where T : class
        {
            try
            {
                var collection = _connection.GetCollection<T>(typeof(T).Name);
                var title = typeof(T).GetProperty("Title");

                if (title != null)
                {
                    var titleValue = title.GetValue(survey);

                    var filter = Builders<T>.Filter.Eq("Title", titleValue);

                    var existingTitle = collection.Find(filter).FirstOrDefault();
                    if (existingTitle != null)
                    {
                        // Title already exists, handle accordingly (e.g., return null or throw an exception)
                        return null;
                    }
                    else
                    {
                        collection.InsertOne(survey);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Nyt kämähti :(  " + err.Message);
            }

            return survey;
        }
    }
}
