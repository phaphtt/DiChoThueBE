using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Market_CSharp.Models
{
    public class CartDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("store_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string store_id { get; set; }

        [BsonElement("product_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string product_id { get; set; }

        [BsonElement("customer_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string customer_id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("price")]
        public int price { get; set; }

        [BsonElement("unit")]
        public string unit { get; set; }

        [BsonElement("amount")]
        public int amount { get; set; }

        [BsonElement("url_image")]
        public string url_image { get; set; }

        [BsonElement("activity")]
        public bool activity { get; set; }
    }

    public class Cart
    {
        public Store store { get; set; }
        public List<CartDetail> products { get; set; }
    }

    public class CartOrder : Cart
    {
        public string customer_id { get; set; }

        public string customer_name { get; set; }

        public string customer_phone { get; set; }

        public string customer_address { get; set; }

        public string customer_region { get; set; }

        public string time { get; set; }

        public int total_amount { get; set; }

        public int shipping_fee { get; set; }

        public int total { get; set; }

        public string payment_type { get; set; }
        
    }
}
