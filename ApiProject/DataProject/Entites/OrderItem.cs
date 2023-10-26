using BaseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    public class OrderItem : BaseModel
    {
        public int Id { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }    
        public int Count { get; set; }
        public double TotalAmount { get; set; }
    }
}
