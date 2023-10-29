using DataProject.Context;
using DataProject.Entites;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    public class DealersController : ControllerBase
    {
        private readonly DbContextClass dbContextClass;
        public DealersController(DbContextClass dbContextClass) 
        {
            this.dbContextClass = dbContextClass;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            return orders;
        }
    }
}
