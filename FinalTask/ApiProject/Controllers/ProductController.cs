using BaseProject.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationProject.Cqrs;
using SchemaProject;

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
        public async Task<ApiResponse<List<ProductResponse>>> GetProducts()
        {
            var handler = new GetAllProductsQuery();
            var result = await mediator.Send(handler);
            return result;
        }

    }
}
