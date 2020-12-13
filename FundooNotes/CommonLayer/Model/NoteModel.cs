﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NoteModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string NoteId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Image { get; set; }

        public string Color { get; set; }

        public bool IsPin { get; set; }

        public bool AddReminder { get; set; }

        public bool IsArchive { get; set; }

        public bool IsTrash { get; set; }
    }
}
