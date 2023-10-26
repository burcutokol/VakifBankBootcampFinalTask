using BaseProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    public class Order : BaseModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public double TotalAmount { get; set; }
        public virtual Dealer Dealer { get; set; }

    }
    //TODO : Dealer konfig
}
