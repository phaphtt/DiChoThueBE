using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market_CSharp.Models
{
    public class InputStatic
    {
        public string store_id { get; set; }
        public string gt_date { get; set; }
        public string lt_date { get; set; }
    }
    public class StaticOrderStore
    {
        public int total_amount { get; set; }
        public int commission { get; set; }
        public List<Order> list_order { get; set; }
    }
}
