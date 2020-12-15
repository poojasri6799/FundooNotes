using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.NoteModels
{
    public class NoteArchive
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string NoteId { get; set; }

        public bool IsArchive { get; set; }
    }
}
