using BaseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    public class Dealer : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string BillInfo { get; set; }    

        public double ProfitMargin { get; set; }
        public double Limit {  get; set; }
    }
}
