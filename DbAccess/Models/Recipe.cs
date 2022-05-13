namespace DbAccess.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using System.Collections.Generic;

    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Instruction { get; set; } = "";
        public string AproxTime { get; set; } = "";

        public string ImgUrl { get; set; } = "";
        public int Servings { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; } = new();
    }
}
