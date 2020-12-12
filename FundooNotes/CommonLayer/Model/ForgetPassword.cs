using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ForgetPassword
    {
        [BsonId]

        [BsonRepresentation(BsonType.ObjectId)]

        public string MailId { get; set; }
    }
}
