using MongoDB.Bson.Serialization.Attributes;
using SharedInterfaces.Interfaces;

namespace SharedInterfaces.Models
{
    public class Ingredient : IIngredient
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
