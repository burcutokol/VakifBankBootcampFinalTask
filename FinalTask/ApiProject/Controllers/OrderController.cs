using BaseProject.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OperationProject.Cqrs;
using SchemaProject;
using System.Security.Claims;

namespace ApiProject.Controllers
{
    [Route("Vk/Api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator mediator;
        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<OrderResponse>>> GetAll()
        {
            var operation = new GetAllOrdersQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetDealersOrders")]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<ApiResponse<OrderResponse>> GetOrderById()
        {
            var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetOrderByIdQuery(int.Parse(id));
            var result = await mediator.Send(operation);
            return result;
        }

    }
}
