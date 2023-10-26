using BaseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    public class Payment : BaseModel
    {
        public int Id { get; set; }
        public string PaymentType { get; set; }

    }
}
