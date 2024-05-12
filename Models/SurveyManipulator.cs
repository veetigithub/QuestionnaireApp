using MongoDB.Bson;
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
                    // testers
                    Console.WriteLine(title.GetValue(survey));
                    Console.WriteLine(survey);
                    Console.WriteLine(title);
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

        public AnsweredSurvey SaveAnsweredSurvey(AnsweredSurvey answeredSurvey)
        {
            try
            {
                var collection = _connection.GetCollection<AnsweredSurvey>("AnsweredSurveys");
                Console.WriteLine("mitää"+collection);
                collection.InsertOne(answeredSurvey);
            }
            catch (Exception err)
            {
                Console.WriteLine("Nyt kämähti :(  " + err.Message);
            }

            return answeredSurvey;
        }
        public List<T> GetAllSurveys<T>(string table)
        {
            var collection = _connection.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public Survey GetByObjectId(ObjectId id) 
        {
            var collection = _connection.GetCollection<Survey>("Survey");
            var filter = Builders<Survey>.Filter.Eq("_id", id);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }

        public List<AnsweredSurvey> GetAnsweredSurveys()
        {
            var collection = _connection.GetCollection<AnsweredSurvey>("AnsweredSurveys");
            return collection.Find(_ => true).ToList();
        }
    }
}

