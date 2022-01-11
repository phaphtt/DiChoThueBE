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
    public class Product : Controller
    {
        // GET: api/product/list/store_id
        [HttpGet("list/{store_id}")]
        public IEnumerable<Models.StoreProduct> Get(string store_id)
        {
            Models.MongoHelper.ConnectToMongoService();
            var store = Models.MongoHelper.database.GetCollection<Models.Store>("store");
            var product = Models.MongoHelper.database.GetCollection<Models.Product>("product");
            var list = new Models.StoreProduct();
            list.store = store.Find(s => s.id == store_id).First();
            list.products = product.Find(s => s.store_id == store_id).ToList();
            yield return list;
        }


        [HttpGet("infor/{product_id}")]
        public IEnumerable<Models.Product> Infor(string product_id)
        {
            Models.MongoHelper.ConnectToMongoService();
            var product = Models.MongoHelper.database.GetCollection<Models.Product>("product");
            yield return product.Find(s => s.id == product_id).ToList().FirstOrDefault();
        }
    }
}
