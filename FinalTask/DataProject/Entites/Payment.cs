using BaseProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    [Table("Payment", Schema = "dbo")]
    public class Payment : BaseModel
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public int BillId { get; set; }
        public virtual Bill Bill {get; set; }
        public double PaidAmount { get; set; }



    }
}
