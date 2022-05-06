namespace DbAccess.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class Ingredient
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
