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
    public class Order : Controller
    {
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("add")]
        public int AddOrder([FromBody] Models.CartOrder cart)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var order = Models.MongoHelper.database.GetCollection<Models.Order>("order");
            var order_item = Models.MongoHelper.database.GetCollection<Models.OrderItem>("order_item");

            Models.Order order_new = new Models.Order();
            order_new.customer_id = cart.customer_id;
            order_new.customer_name = cart.customer_name;
            order_new.customer_phone = cart.customer_phone;
            order_new.customer_address = cart.customer_address;
            order_new.customer_region = cart.customer_region;
            order_new.order_date = DateTime.Today;
            order_new.time = cart.time;
            order_new.payment_type = cart.payment_type;
            order_new.total_amount = cart.total_amount;
            order_new.shipping_fee = cart.shipping_fee;
            order_new.total = cart.total;
            order_new.status = "Chưa xác nhận";
            order_new.activity = true;
            order_new.store_id = cart.store.id;
            order_new.cancel = false;
            order.InsertOne(order_new);

            
            foreach (var item in cart.products)
            {
                Models.OrderItem order_item_new = new Models.OrderItem();
                order_item_new.order_id = order_new.id;
                order_item_new.product_id = item.product_id;
                order_item_new.product_name = item.name;
                order_item_new.price = item.price;
                order_item_new.amount = item.amount;
                order_item_new.total = item.price * item.amount;
                order_item_new.cancel = false;
                order_item_new.order_date = DateTime.Today;
                order_item.InsertOne(order_item_new);
            }
            return 1;
        }
    }
}
