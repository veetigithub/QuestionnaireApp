using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireApp.Models
{
    public class User
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
