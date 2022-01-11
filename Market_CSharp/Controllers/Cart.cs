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
    public class Cart : Controller
    {
        // GET: api/cart/customer_id
        [HttpGet("{customer_id}")]
        public IEnumerable<Models.Cart> GetDetail(string customer_id)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var product = Models.MongoHelper.database.GetCollection<Models.CartDetail>("cart_detail");
            var store = Models.MongoHelper.database.GetCollection<Models.Store>("store");

            //Load add products in cart of customer, if activity = true:
            List<Models.CartDetail> products = new List<Models.CartDetail>();
            products = product.Find(p => p.activity == true && p.customer_id == customer_id).ToList();

            //Load store_id in cart:
            List<string> store_id = new List<string>();
            foreach (var temp_product in products)
            {
                bool check = false;
                foreach (var temp_store in store_id)
                {
                    if (temp_product.store_id == temp_store)
                    {
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    store_id.Add(temp_product.store_id);
                }
            }

            //return cart:
            var cart = new Models.Cart();
            foreach(string id in store_id)
            {
                cart.store = store.Find(s => s.id == id).FirstOrDefault();
                cart.products = product.Find(p => p.activity == true && p.store_id == id && p.customer_id == customer_id).ToList();
                yield return cart;
            }
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("{customer_id}/add")]
        public int AddCart(string customer_id, [FromBody] Models.Product product)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var item_cart = Models.MongoHelper.database.GetCollection<Models.CartDetail>("cart_detail");
            var item = item_cart.Find(i => i.activity == true && i.customer_id == customer_id && i.product_id == product.id);


            var item_new = new Models.CartDetail();
            if (item.CountDocuments() == 0)
            {
                item_new.store_id = product.store_id;
                item_new.product_id = product.id;
                item_new.customer_id = customer_id;
                item_new.name = product.name;
                item_new.price = product.price;
                item_new.unit = product.unit;
                item_new.amount = 1;
                item_new.url_image = product.url_image;
                item_new.activity = true;
                item_cart.InsertOne(item_new);
            }
            else
            {
                if (item.FirstOrDefault().amount == 5)
                    return -1;
                var filter = Builders<CartDetail>.Filter.Where(c => c.activity == true && c.customer_id == customer_id && c.product_id == product.id);
                int amount = item.FirstOrDefault().amount + 1;
                var update = Builders<CartDetail>.Update.Set("amount", amount);
                item_cart.UpdateOne(filter, update);
            }
           return 1;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPatch("{customer_id}/update")]
        public int UpdateCart(string customer_id, [FromBody] Models.CartDetail item)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var item_cart = Models.MongoHelper.database.GetCollection<Models.CartDetail>("cart_detail");

            if (item.amount > 5)
                return -1;
            var filter = Builders<CartDetail>.Filter.Where(c => c.id == item.id);
            var update = Builders<CartDetail>.Update.Set("amount", item.amount);
            item_cart.UpdateOne(filter, update);
            return 1;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpDelete("{customer_id}/delete")]
        public int DeleteCart(string customer_id, [FromBody] Models.CartDetail item)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var item_cart = Models.MongoHelper.database.GetCollection<Models.CartDetail>("cart_detail");

            if (item.activity == false)
                return -1;
            var filter = Builders<CartDetail>.Filter.Where(c => c.id == item.id);
            var update = Builders<CartDetail>.Update.Set("activity", false);
            item_cart.UpdateOne(filter, update);
            return 1;
        }
    }
}
