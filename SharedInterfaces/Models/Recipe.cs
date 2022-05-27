﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using SharedInterfaces.Interfaces;
using System.Collections.Generic;

namespace SharedInterfaces.Models
{
    public class Recipe : IRecipe
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Instruction { get; set; } = "";
        public int AproxTime { get; set; }

        public string ImgUrl { get; set; } = "";
        public int Servings { get; set; }
        public IEnumerable<string> Instructions { get; set; }
        //[JsonConverter(typeof(ConcreteConverter<Ingredient>))]
        [BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<IEnumerable<IIngredientWithAmount>, List<IngredientWithAmount>>))]

        public IEnumerable<IIngredientWithAmount> Ingredients { get; set; } = new List<IIngredientWithAmount>();
    }
}
