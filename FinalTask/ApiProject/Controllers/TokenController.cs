using BaseProject.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationProject.Command;
using OperationProject.Cqrs;
using SchemaProject;

namespace ApiProject.Controllers
{
    [Route("Vk/Api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IMediator mediator;
        public TokenController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
        {
            var handler = new CreateTokenCommand(request);
            var result = await mediator.Send(handler);
            return result;
        }

    }
}
