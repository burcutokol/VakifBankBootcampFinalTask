using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class PaymentRequest
    {
        public string PaymentType { get; set; }
        public int OrderId { get; set; }

    }
    public class PaymentResponse 
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentType { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentStatus { get; set; }
    }

}
