using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace QuestionnaireApp.Models
{
    public class Survey
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public List<string> Questions { get; set; }
    }
}
