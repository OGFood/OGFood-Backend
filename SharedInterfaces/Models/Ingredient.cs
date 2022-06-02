using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using SharedInterfaces.Interfaces;

namespace SharedInterfaces.Models
{
    public class Ingredient : IIngredient
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
