using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace QuestionnaireApp.Models
{
    public class AnsweredSurvey
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public ObjectId SurveyId { get; set; }
        public string UserId { get; set; }
        public List<string> UserAnswers { get; set; }
    }
}
