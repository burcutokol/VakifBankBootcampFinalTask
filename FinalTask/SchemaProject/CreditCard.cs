using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class CreditCard : PaymentRequest
    {
        public string CardId { get; set; }
        public string CustomerName { get; set; }
        public string CVV { get; set; }
    }
}
