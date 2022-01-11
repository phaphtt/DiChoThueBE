using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Market_CSharp.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("store_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string store_id { get; set; }

        [BsonElement("type_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string type_id { get; set; }

        [BsonElement("type_name")]
        public string type_name { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("price")]
        public int price { get; set; }

        [BsonElement("unit")]
        public string unit { get; set; }

        [BsonElement("origin")]
        public string origin { get; set; }

        [BsonElement("status")]
        public string status { get; set; }

        [BsonElement("url_image")]
        public string url_image { get; set; }

        [BsonElement("activity")]
        public bool activity { get; set; }

        [BsonElement("create_date")]
        public DateTime create_date { get; set; }

        [BsonElement("update_date")]
        public DateTime update_date { get; set; }
    }

    public class ProductStaticNecessary : Product 
    {
        public int amount_sale { get; set; }
    }

    public class ProductStaticYearAgoPeriod : Product
    {
        public int amount_sale_ago { get; set; }
        public int amount_sale_now { get; set; }
    }
}
