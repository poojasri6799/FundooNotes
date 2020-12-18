using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.NoteModels
{
    public class Colour
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Color { get; set; }
    }
}
