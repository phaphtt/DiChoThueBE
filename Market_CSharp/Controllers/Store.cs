using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market_CSharp.Models;
using MongoDB.Driver;

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
    }
}
