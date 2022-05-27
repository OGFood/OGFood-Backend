using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using SharedInterfaces.Interfaces;

namespace SharedInterfaces.Models
{
    public class IngredientWithAmount : IIngredientWithAmount
    {
        //[JsonConverter(typeof(ConcreteConverter<Ingredient>))]
        [BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<IIngredient, Ingredient>))]
        public IIngredient Ingredient { get; set; } = new Ingredient();
        public double Amount { get; set; }
        public string Unit { get; set; } = "";
    }
}
