using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaProject
{
    public class ReportRequest
    {
        public int DealerId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

    }
    public class ReportResponse
    {
        public int DealerId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

    }
}
