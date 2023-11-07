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
        //TODO getorderbyid
        [HttpGet("GetDealersOrders")]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<OrderResponse>> GetDealersOrders()
        {
            var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetOrderByIdQuery(int.Parse(id));
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpPost("CreateOrder")]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse<OrderResponse>> CreateOrder([FromBody] OrderRequest model)
        {
            var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new CreateOrderCommand(model, int.Parse(id));
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpPost("CancelOrder")]
        [Authorize(Roles = "Dealer")]
        public async Task<ApiResponse> CancelOrder(int orderId)
        {
            var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateOrderCommand(orderId, int.Parse(id));
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpPost("OrderApproveReject")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> ApproveRejectOrder(int orderId, bool status)
        {
            IRequest<ApiResponse> operation;
            if (status)
                operation = new ApproveOrder(orderId);
            else
                operation = new RejectOrder(orderId);

            var result = await mediator.Send(operation);
            return result;
        }
      

    }
}
