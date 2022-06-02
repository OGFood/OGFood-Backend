using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using SharedInterfaces.Interfaces;
using System.Collections.Generic;

namespace SharedInterfaces.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int AproxTime { get; set; }
        public string ImgUrl { get; set; } = "";
        public int Servings { get; set; }
        public IEnumerable<string> Instructions { get; set; } = new List<string>();

        public IEnumerable<IngredientWithAmount> Ingredients { get; set; } = new List<IngredientWithAmount>();
    }
}