using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Market_CSharp.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("store_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string store_id { get; set; }

        [BsonElement("customer_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string customer_id { get; set; }


        [BsonElement("customer_name")]
        public string customer_name { get; set; }

        [BsonElement("customer_phone")]
        public string customer_phone { get; set; }

        [BsonElement("customer_address")]
        public string customer_address { get; set; }

        [BsonElement("customer_region")]
        public string customer_region { get; set; }

        [BsonElement("time")]
        public string time { get; set; }

        [BsonElement("order_date")]
        public DateTime order_date { get; set; }

        [BsonElement("payment_type")]
        public string payment_type { get; set; }

        [BsonElement("total_amount")]
        public int total_amount { get; set; }

        [BsonElement("shipping_fee")]
        public int shipping_fee { get; set; }

        [BsonElement("total")]
        public int total { get; set; }

        [BsonElement("status")]
        public string status { get; set; }

        [BsonElement("cancel")]
        public bool cancel { get; set; }

        [BsonElement("activity")]
        public bool activity { get; set; }
    }

    public class OrderItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("order_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string order_id { get; set; }

        [BsonElement("product_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string product_id { get; set; }

        [BsonElement("product_name")]
        public string product_name { get; set; }

        [BsonElement("price")]
        public int price { get; set; }

        [BsonElement("amount")]
        public int amount { get; set; }

        [BsonElement("total")]
        public int total { get; set; }

        [BsonElement("cancel")]
        public bool cancel { get; set; }

        [BsonElement("order_date")]
        public DateTime order_date { get; set; }
    }
}
