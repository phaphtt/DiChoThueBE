using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market_CSharp.Models;
using Market_CSharp.Metric;
using MongoDB.Driver;
using Microsoft.AspNetCore.Cors;
using System.Globalization;

namespace Market_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Static : Controller
    {
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("store")]
        public IEnumerable<Models.StaticOrderStore> StaticOrder([FromBody] Models.InputStatic response)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var order = Models.MongoHelper.database.GetCollection<Models.Order>("order");

            DateTime gt = DateTime.ParseExact(response.gt_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime lt = DateTime.ParseExact(response.lt_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var filterBuilder = Builders<Models.Order>.Filter;
            var filter = filterBuilder.Gt("order_date", gt) &
                         filterBuilder.Lt("order_date", lt) &
                         filterBuilder.Where(temp => temp.store_id == response.store_id && temp.activity == true && temp.cancel == false);
            StaticOrderStore static_store = new Models.StaticOrderStore();
            static_store.list_order = order.Find(filter).ToList();
            foreach (var temp in static_store.list_order)
            {
                static_store.total_amount += temp.total_amount;
            }
            static_store.commission = static_store.total_amount * 15 /100;
            yield return static_store;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("product/now/{date}")]
        public IEnumerable<Models.ProductStaticNecessary> StaticProductNecessary(string date)
        {
            List<Models.ProductStaticNecessary> product = Metric.Product.FilterProduct(date);
            for (int i = 0; i < 10; i++)
            {
                yield return product[i];
            }
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet("product/agoPeriod/{date}")]
        public IEnumerable<Models.ProductStaticYearAgoPeriod> StaticProductYearAgePeriod(string date)
        {
            List<Models.ProductStaticYearAgoPeriod> product = Metric.Product.FilterProductYearAgoPeriod(date);
            for (int i = 0; i < 10; i++)
            {
                yield return product[i];
            }
        }
    }
}
