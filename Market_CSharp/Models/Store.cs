using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Market_CSharp.Models
{
    public class Store
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("store_name")]
        public string store_name { get; set; }

        [BsonElement("phone")]
        public string phone { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("address")]
        public string address { get; set; }

        [BsonElement("person_name")]
        public string person_name { get; set; }

        [BsonElement("person_phone")]
        public string person_phone { get; set; }

        [BsonElement("region")]
        public string region { get; set; }

        [BsonElement("username")]
        public string username { get; set; }


        [BsonElement("password")]
        public string password { get; set; }

        [BsonElement("contract_start")]
        public string contract_start { get; set; }

        [BsonElement("contract_end")]
        public string contract_end { get; set; }

        [BsonElement("rules")]
        public string rules { get; set; }

        [BsonElement("url_image")]
        public string url_image { get; set; }

        [BsonElement("activity")]
        public bool activity { get; set; }

        [BsonElement("create_date")]
        public DateTime create_date { get; set; }

        [BsonElement("update_date")]
        public DateTime update_date { get; set; }
    }

    public class StoreProduct
    {
        public Store store { get; set; }
        public List<Product> products { get; set; }
    }
}
