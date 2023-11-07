using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
    public class OrderItemResponse
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public ProductResponse Product { get; set; }
    }
}
