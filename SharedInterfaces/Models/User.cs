using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using SharedInterfaces.Interfaces;

namespace SharedInterfaces.Models
{
    public class User //: IUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Mail { get; set; } = "";
        public string Salt { get; set; } = "";
        public string Password { get; set; } = "";
        //[BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<IEnumerable<IIngredient>, List<Ingredient>>))]
        public IEnumerable<Ingredient> Cupboard { get; set; } = new List<Ingredient>();
    }
}
