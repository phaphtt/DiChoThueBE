using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market_CSharp.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Cors;

namespace Market_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Store : Controller
    {
        // GET: api/store/list
        [HttpGet("list")]
        public IEnumerable<Models.Store> GetList()
        {
            Models.MongoHelper.ConnectToMongoService();
            var store = Models.MongoHelper.database.GetCollection<Models.Store>("store");
            return store.Find(s => s.activity == true).ToList();
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPatch("order/confirm/{order_id}")]
        public int ConfirmOrder(string order_id)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var order = Models.MongoHelper.database.GetCollection<Models.Order>("order");
            
            var exists = order.Find(o => o.activity == true && o.status == "Xác nhận" && o.id == order_id);

            if (exists.CountDocuments() != 0)
                return -1;

            var filter = Builders<Models.Order>.Filter.Where(c => c.id == order_id);
            var update = Builders<Models.Order>.Update.Set("status", "Xác nhận");
            order.UpdateOne(filter, update);
            return 1;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPatch("order/refuse/{order_id}")]
        public int Refuse(string order_id)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var order = Models.MongoHelper.database.GetCollection<Models.Order>("order");
            var order_item = Models.MongoHelper.database.GetCollection<Models.OrderItem>("order_item");

            var exists = order.Find(o => o.activity == true && o.status == "Từ chối" && o.id == order_id);

            if (exists.CountDocuments() != 0)
                return -1;

            var filter1 = Builders<Models.Order>.Filter.Where(c => c.id == order_id);
            var update1 = Builders<Models.Order>.Update.Set("status", "Từ chối").Set("cancel", true);
            order.UpdateOne(filter1, update1);

            var filter2 = Builders<Models.OrderItem>.Filter.Where(c => c.order_id == order_id);
            var update2 = Builders<Models.OrderItem>.Update.Set("cancel", true);
            order_item.UpdateMany(filter2, update2);
            return 1;
        }
    }
}
