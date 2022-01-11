using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace Market_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductType : Controller
    {
        // GET: api/producttype/list
        [HttpGet("list")]
        public IEnumerable<Models.ProductType> ListProductType()
        {
            Models.MongoHelper.ConnectToMongoService();
            var product_type = Models.MongoHelper.database.GetCollection<Models.ProductType>("product_type");
            return product_type.Find(t => t.activity == true).ToList();
        }
    }
}
