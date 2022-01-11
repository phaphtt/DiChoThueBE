using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market_CSharp.Models
{
    public class ProductInsert
    {
        public string store_id { get; set; }

        public string type_id { get; set;}

        public string product_name { get; set; }

        public int price { get; set; }
    
        public string unit { get; set; }

        public string origin { get; set; }

        public string url_image { get; set; }
    }
}
