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
    public class ProductController : ControllerBase
    {
        private IMediator mediator;
        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<ProductResponse>>> GetProducts()
        {
            var handler = new GetAllProductsQuery();
            var result = await mediator.Send(handler);
            return result;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Dealer")]
        public async Task<ApiResponse<ProductResponse>> GetProductById(int id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var handler = new GetProductByIdQuery(id, int.Parse(userId));
            var result = await mediator.Send(handler);
            return result;
        }
        [HttpPost("UpdateProductStock{productid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse> UpdateProduct(int productId, int stockNumber)
        {
            var handler = new UpdateProductCommand(productId, stockNumber);
            var result = await mediator.Send(handler);
            return result;
        }
        [HttpPost("UpdateProductStockByOrder")]
        public async Task<ApiResponse> UpdateProductByOrder(int orderId, bool status)
        {
            var handler = new UpdateProductStockByOrderCommand(orderId, status);
            var result = await mediator.Send(handler);
            return result;
        }




    }
}
