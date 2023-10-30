using DataProject.Context;
using DataProject.Entites;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    public class DealerController : ControllerBase
    {
        private readonly DbContextClass dbContextClass;
        public DealerController(DbContextClass dbContextClass) 
        {
            this.dbContextClass = dbContextClass;
        }

        [HttpGet]
        public List<Order> GetDealers()
        {
            List<Order> orders = new List<Order>();
            return orders;
        }
    }
}
