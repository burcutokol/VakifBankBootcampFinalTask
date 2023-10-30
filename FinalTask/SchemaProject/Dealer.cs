using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class DealerRequest
    {
        public int DealerId { get; set; }
        public int UserLoginId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double ProfitMargin { get; set; }
        public double Limit { get; set; }

    }
    public class DealerResponse : DealerRequest
    {
        public virtual List<OrderResponse> Orders { get; set; }
        public virtual List<ReportResponse> Reports { get; set; }
        public virtual List<BillResponse> Bills { get; set; }
    }
}
