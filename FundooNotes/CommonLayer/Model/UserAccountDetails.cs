﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class UserAccountDetails
    {
        [BsonId]

        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Please enter a valid first name ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Please enter a valid  last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+([.][a-zA-Z0-9]+)?@[a-zA-Z0-9]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2})?$", ErrorMessage = "Please enter a valid email address")]
        public string MailId { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
