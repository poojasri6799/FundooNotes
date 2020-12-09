﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserAccountDetails
    {
        [BsonId]

        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MailId { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
