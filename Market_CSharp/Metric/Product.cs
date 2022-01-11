using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market_CSharp.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Cors;
using System.Globalization;

namespace Market_CSharp.Metric
{
    public class Product
    {
        public static List<Models.ProductStaticNecessary> FilterProduct(string date)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var productdb = Models.MongoHelper.database.GetCollection<Models.Product>("product");
            var orderItemDb = Models.MongoHelper.database.GetCollection<Models.OrderItem>("order_item");

            DateTime date_convert = DateTime.ParseExact(date, "yyyy-MM", CultureInfo.InvariantCulture);
            int month = date_convert.Month;
            int year = date_convert.Year;

            var filterBuilderProduct = Builders<Models.Product>.Filter;
            var filterProduct = filterBuilderProduct.Where(product => product.activity == true);


            List<Models.ProductStaticNecessary> list = new List<Models.ProductStaticNecessary>();
            List<Models.Product> product = productdb.Find(filterProduct).ToList();
            foreach (var temp in product)
            {
                var filterBuilderOrderItem = Builders<Models.OrderItem>.Filter;
                var filter = filterBuilderOrderItem.Where(t => t.cancel == false && t.product_id == temp.id);
                List<Models.OrderItem> orderItem = orderItemDb.Find(filter).ToList();
                int amountSale = 0;
                foreach (var item in orderItem)
                {
                    if (item.order_date.Month == month && item.order_date.Year == year)
                        amountSale += item.amount;
                }
                ProductStaticNecessary static_product = new Models.ProductStaticNecessary();
                static_product.id = temp.id;
                static_product.store_id = temp.store_id;
                static_product.type_id = temp.type_id;
                static_product.type_name = temp.type_name;
                static_product.name = temp.name;
                static_product.price = temp.price;
                static_product.unit = temp.unit;
                static_product.status = temp.status;
                static_product.url_image = temp.url_image;
                static_product.activity = temp.activity;
                static_product.create_date = temp.create_date;
                static_product.update_date = temp.update_date;
                static_product.amount_sale = amountSale;
                list.Add(static_product);
            }
            return list.OrderByDescending(l => l.amount_sale).ToList();
        }

        public static List<Models.ProductStaticYearAgoPeriod> FilterProductYearAgoPeriod(string date)
        {
            //Connect DB:
            Models.MongoHelper.ConnectToMongoService();
            var productdb = Models.MongoHelper.database.GetCollection<Models.Product>("product");
            var orderItemDb = Models.MongoHelper.database.GetCollection<Models.OrderItem>("order_item");

            DateTime date_convert = DateTime.ParseExact(date, "yyyy-MM", CultureInfo.InvariantCulture);
            int month = date_convert.Month;
            int year = date_convert.Year;

            var filterBuilderProduct = Builders<Models.Product>.Filter;
            var filterProduct = filterBuilderProduct.Where(product => product.activity == true);


            List<Models.ProductStaticYearAgoPeriod> list = new List<Models.ProductStaticYearAgoPeriod>();
            List<Models.Product> product = productdb.Find(filterProduct).ToList();
            foreach (var temp in product)
            {
                var filterBuilderOrderItem = Builders<Models.OrderItem>.Filter;
                var filter = filterBuilderOrderItem.Where(t => t.cancel == false && t.product_id == temp.id);
                List<Models.OrderItem> orderItem = orderItemDb.Find(filter).ToList();
                int amountSaleNow = 0;
                int amountSaleAgo = 0;
                foreach (var item in orderItem)
                {
                    if (item.order_date.Month == month && item.order_date.Year == year)
                        amountSaleNow += item.amount;
                    else if(item.order_date.Month == month && item.order_date.Year == year - 1)
                        amountSaleAgo += item.amount;
                }
                ProductStaticYearAgoPeriod static_product = new Models.ProductStaticYearAgoPeriod();
                static_product.id = temp.id;
                static_product.store_id = temp.store_id;
                static_product.type_id = temp.type_id;
                static_product.type_name = temp.type_name;
                static_product.name = temp.name;
                static_product.price = temp.price;
                static_product.unit = temp.unit;
                static_product.status = temp.status;
                static_product.url_image = temp.url_image;
                static_product.activity = temp.activity;
                static_product.create_date = temp.create_date;
                static_product.update_date = temp.update_date;
                static_product.amount_sale_now = amountSaleNow;
                static_product.amount_sale_ago = amountSaleAgo;
                list.Add(static_product);
            }
            return list.OrderByDescending(l => l.amount_sale_now).ToList();
        }
    }
}
