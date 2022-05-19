﻿namespace DbAccess.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using SharedInterfaces.Interfaces;
    using System.Collections.Generic;

    public class Recipe : IRecipe
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> Instructions { get; set; } = new();
        public int AproxTime { get; set; }

        public string ImgUrl { get; set; } = "";
        public int Servings { get; set; }
        public IEnumerable<string> Instructions { get; set; }
        public IEnumerable<IIngredientWithAmount> Ingredients { get; set; }
    }
}
