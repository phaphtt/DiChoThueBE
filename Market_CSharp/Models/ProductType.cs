using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Market_CSharp.Models
{
    public class ProductType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("activity")]
        public bool activity { get; set; }

        [BsonElement("create_date")]
        public DateTime create_date { get; set; }

        [BsonElement("update_date")]
        public DateTime update_date { get; set; }
    }
}
