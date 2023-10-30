using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int PaymentId { get; set; }
        public int DealerId { get; set; }
        public double TotalAmount { get; set; }
    }
    public class OrderResponse : OrderRequest
    {
        public virtual List<OrderItemResponse> Items { get; set; }
    }
}
