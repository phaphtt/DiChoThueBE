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

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("product/add")]
        public int AddProduct([FromBody] Models.ProductInsert p)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var product = Models.MongoHelper.database.GetCollection<Models.Product>("product");
            var product_type = Models.MongoHelper.database.GetCollection<Models.ProductType>("product_type");

            var exists = product.Find(o => o.activity == true && o.name == p.product_name && o.store_id == p.store_id);

            if (exists.CountDocuments() != 0)
                return -1;

            var type = product_type.Find(o => o.id == p.type_id);
            Models.Product product_new = new Models.Product();

            product_new.store_id = p.store_id;
            product_new.type_id = p.type_id;
            product_new.type_name = type.FirstOrDefault().name;
            product_new.name = p.product_name;
            product_new.price = p.price;
            product_new.unit = p.unit;
            product_new.status = "";
            product_new.activity = true;
            product_new.origin = p.origin;
            product_new.url_image = p.url_image;
            product_new.create_date = DateTime.Today;
            product_new.update_date = DateTime.Today;
            product.InsertOne(product_new);
            return 1; 
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPatch("product/update/{product_id}")]
        public int UpdateProduct(string product_id, [FromBody] Models.ProductInsert p)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var product = Models.MongoHelper.database.GetCollection<Models.Product>("product");
            var product_type = Models.MongoHelper.database.GetCollection<Models.ProductType>("product_type");

            var exists = product.Find(o => o.id == product_id);

            if (exists.CountDocuments() == 0)
                return -1;

            var type = product_type.Find(o => o.id == p.type_id);
            var filter = Builders<Models.Product>.Filter.Where(c => c.id == product_id);
            var update = Builders<Models.Product>.Update.Set("activity", exists.FirstOrDefault().activity)
                                                        .Set("type_id", p.type_id)
                                                        .Set("type_name", type.FirstOrDefault().name)
                                                        .Set("name", p.product_name)
                                                        .Set("price", p.price)
                                                        .Set("unit", p.unit)
                                                        .Set("origin", p.origin)
                                                        .Set("url_image", p.url_image)
                                                        .Set("update_date", DateTime.Today);
            product.UpdateOne(filter, update);
            return 1;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpDelete("product/delete/{product_id}")]
        public int DeleteProduct(string product_id)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var product = Models.MongoHelper.database.GetCollection<Models.Product>("product");
           
            var filter = Builders<Models.Product>.Filter.Where(c => c.id == product_id);
            var update = Builders<Models.Product>.Update.Set("activity", false);
            product.UpdateOne(filter, update);
            return 1;
        }
    }
}
