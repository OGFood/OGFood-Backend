﻿using MongoDB.Bson.Serialization.Attributes;
using SharedInterfaces.Interfaces;
using System.Collections.Generic;

namespace SharedInterfaces.Models
{
    public class User : IUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Mail { get; set; } = "";
        public string Salt { get; set; } = "";
        public string Password { get; set; } = "";
        public IEnumerable<IIngredient> Cupboard { get; set; }
    }
}
