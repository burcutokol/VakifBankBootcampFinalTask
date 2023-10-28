using BaseProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    [Table("OrderItem", Schema = "dbo")]
    public class OrderItem : BaseModel
    {
        public int OrderId { get; set; } //required 
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }    
        public int Count { get; set; }
        public double TotalAmount { get; set; }
    }
}
