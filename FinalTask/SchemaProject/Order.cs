using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class OrderRequest
    {
        public virtual List<OrderItemRequest> Items { get; set; }
    }
    public class OrderResponse 
    {
        public int PaymentId { get; set; }
        public int DealerId { get; set; }
        public double TotalAmount { get; set; }
        public virtual List<OrderItemResponse> Items { get; set; }
    }
}
