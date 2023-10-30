using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class BillRequest
    {
        public int BillId { get; set; }
        public int PaymentId { get; set; }
        public double TotalAmount { get; set; }
        public int DealerId { get; set; }
    }
    public class BillResponse : BillRequest
    {
        public virtual List<OrderResponse> Orders { get; set; }
        public virtual List<ReportResponse> Reports { get; set; }
    }
}
